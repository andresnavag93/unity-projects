using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [HideInInspector]
    public bool gamePlaying;

    [SerializeField]
    private GameObject tile;
    private Vector3 currentTilePosition;

    private AudioSource audioSource;

    [SerializeField]
    private Material tileMat;

    [SerializeField]
    private Light dayLight;

    private Camera mainCamera;
    private bool camColorLerp;
    private Color camColor;
    private Color[] tileColorDay;
    private Color tileColorNight;
    private int tileColorIndex;

    private Color tileTrueColor;
    private float timer;
    private float timerInterval = 10f;

    private float camLerpTimer;
    private float camLerpInterval = 1f;

    private int direction = 1;

    void Awake()
    {
        MakeSingleton();
        audioSource = GetComponent<AudioSource>();
        currentTilePosition = new Vector3(-2, 0, 2);

        mainCamera = Camera.main;
        camColor = mainCamera.backgroundColor;
        tileTrueColor = tileMat.color;

        tileColorIndex = 0;
        tileColorDay = new Color[3];
        tileColorDay[0] = new Color(10 / 256f, 139 / 256f, 203 / 256f);
        tileColorDay[1] = new Color(10 / 256f, 200 / 256f, 20 / 256f);
        tileColorDay[2] = new Color(220 / 256f, 170 / 256f, 45 / 256f);
        tileColorNight = new Color(0, 8 / 256f, 11 / 256f);

        tileMat.color = tileColorDay[0];

    }

    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            CreateTiles();
        }
    }

    private void Update()
    {
        CheckLerpTimer();
    }

    private void OnDisable()
    {
        instance = null;
        tileMat.color = tileTrueColor;
    }

    void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        };

    }

    void CreateTiles()
    {
        Vector3 newTilePosition = currentTilePosition;
        int rand = Random.Range(0, 100);

        if (rand < 50)
        {
            newTilePosition.x -= 1f;
        }
        else
        {
            newTilePosition.z += 1f;
        }

        currentTilePosition = newTilePosition;
        Instantiate(tile, currentTilePosition, Quaternion.identity);
    }

    void CheckLerpTimer()
    {
        timer += Time.deltaTime;
        if (timer > timerInterval)
        {
            timer -= timerInterval;
            camColorLerp = true;
            camLerpTimer = 0f;
        }

        if (camColorLerp)
        {
            camLerpTimer += Time.deltaTime;
            float percent = camLerpTimer / camLerpInterval;

            if (direction == 1)
            {
                mainCamera.backgroundColor = Color.Lerp(camColor, Color.black, percent);
                tileMat.color = Color.Lerp(tileColorDay[tileColorIndex], tileColorNight, percent);
                dayLight.intensity = 1f - percent;
            }
            else
            {
                mainCamera.backgroundColor = Color.Lerp(Color.black, camColor, percent);
                tileMat.color = Color.Lerp(tileColorNight, tileColorDay[tileColorIndex], percent);
                dayLight.intensity = percent;
            }

            if (percent > 0.98f)
            {
                camLerpTimer = 1f;
                direction *= -1;
                camColorLerp = false;
                if (direction == -1)
                {
                    tileColorIndex = Random.Range(0, tileColorDay.Length);
                }
            }

        }
    }

    public void ActiveTileSpawner()
    {
        StartCoroutine(SpawnNewTiles());
    }

    IEnumerator SpawnNewTiles()
    {
        yield return new WaitForSeconds(0.3f);
        CreateTiles();

        if (gamePlaying)
        {
            StartCoroutine(SpawnNewTiles());
        }
    }

    public void PlayCollectableSound()
    {
        audioSource.Play();
    }
} //class
