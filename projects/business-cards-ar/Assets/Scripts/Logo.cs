using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    public Animator playerAnim;
    public int trigger = 4;
    public bool animator = true, isComplete = true;
    public GameObject wCore, wYellow, wRed, wBlue, wDarkBlue, FrontBusinessCard;
    public Vector3 wCoreP, wYellowP, wRedP, wBlueP, wDarkBlueP;
    Vector3 wCoreRotation = new Vector3(0, 180, 0);
    public GameObject show_cloud, show_broom;
    private int already = 0;

    public bool show = true;

    // Start is called before the first frame update
    void Start()
    {
        wCoreP = wCore.transform.localPosition;
        wYellowP = wYellow.transform.localPosition;
        wRedP = wRed.transform.localPosition;
        wBlueP = wBlue.transform.localPosition;
        wDarkBlueP = wDarkBlue.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isComplete)
        {
            if (animator)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (trigger == 1)
                    {
                        StartCoroutine(JumpLogo("roll", 2.0f, 2));
                    }
                    else if (trigger == 2)
                    {
                        StartCoroutine(JumpLogo("jump", 2.0f, 3));
                    }
                    else if (trigger == 3)
                    {
                        StartCoroutine(JumpLogo("scale", 2.0f, 4));
                    }
                    else if (trigger == 4)
                    {
                        if (show)
                        {
                            playerAnim.enabled = false;
                            wCore.transform.eulerAngles = wCoreRotation;
                            trigger = 0;
                            JigSawPuzzleBegin();
                            if (already == 0)
                            {
                                StartCoroutine(ActiveCloud2(9));
                            }
                        }
                        else
                        {
                            trigger = 1;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isComplete)
        {
            if (other.gameObject.name == "wYellow")
            {
                other.transform.SetParent(wCore.transform);
                other.transform.position = new Vector3(
                    wCore.transform.position.x + 10f,
                    wCore.transform.position.y,
                    wCore.transform.position.z + 10f);
            }
            else if (other.gameObject.name == "wRed")
            {
                other.transform.SetParent(wCore.transform);
                other.transform.position = new Vector3(
                    wCore.transform.position.x - 10f,
                    wCore.transform.position.y,
                    wCore.transform.position.z + 10f);
            }
            else if (other.gameObject.name == "wBlue")
            {
                other.transform.SetParent(wCore.transform);
                other.transform.position = new Vector3(
                    wCore.transform.position.x + 10f,
                    wCore.transform.position.y,
                    wCore.transform.position.z - 10f);
            }
            else if (other.gameObject.name == "wDarkBlue")
            {
                other.transform.SetParent(wCore.transform);
                other.transform.position = new Vector3(
                    wCore.transform.position.x - 10f,
                    wCore.transform.position.y,
                    wCore.transform.position.z - 10f);
            }
            if (wCore.transform.childCount >= 4)
            {
                JigSawPuzzleComplete();
            }
        }
    }

    private void JigSawPuzzleBegin()
    {
        wCoreP = wCore.transform.localPosition;
        wYellowP = wYellow.transform.localPosition;
        wRedP = wRed.transform.localPosition;
        wBlueP = wBlue.transform.localPosition;
        wDarkBlueP = wDarkBlue.transform.localPosition;

        wYellow.transform.SetParent(FrontBusinessCard.transform);
        wRed.transform.SetParent(FrontBusinessCard.transform);
        wBlue.transform.SetParent(FrontBusinessCard.transform);
        wDarkBlue.transform.SetParent(FrontBusinessCard.transform);

        wYellow.transform.position = new Vector3(
            wYellow.transform.position.x + 100f,
            wYellow.transform.position.y,
            wYellow.transform.position.z + 45f);
        wRed.transform.position = new Vector3(
            wRed.transform.position.x - 100f,
            wRed.transform.position.y,
            wRed.transform.position.z + 45f);
        wBlue.transform.position = new Vector3(
            wBlue.transform.position.x + 100f,
            wBlue.transform.position.y,
            wBlue.transform.position.z - 45f);
        wDarkBlue.transform.position = new Vector3(
            wDarkBlue.transform.position.x - 100f,
            wDarkBlue.transform.position.y,
            wDarkBlue.transform.position.z - 45f);
        isComplete = false;
    }

    private void JigSawPuzzleComplete()
    {
        isComplete = true;
        trigger = 1;
        StartCoroutine(WaitSeconds(2));
        wYellow.transform.localPosition = wYellowP;
        wRed.transform.localPosition = wRedP;
        wBlue.transform.localPosition = wBlueP;
        wDarkBlue.transform.localPosition = wDarkBlueP;
        wCore.transform.localPosition = wCoreP;
    }

    private IEnumerator JumpLogo(string name, float seconds, int value)
    {
        animator = false;
        playerAnim.SetTrigger(name);
        yield return new WaitForSeconds(seconds);
        trigger = value;
        animator = true;
    }

    private IEnumerator WaitSeconds(float seconds)
    {
        animator = false;
        playerAnim.enabled = true;
        yield return new WaitForSeconds(seconds);
        animator = true;
    }

    private IEnumerator ActiveCloud2(float seconds)
    {
        show_cloud.SetActive(true);
        already = 1;
        yield return new WaitForSeconds(seconds);
        show_cloud.SetActive(false);

        show_broom.SetActive(true);
        yield return new WaitForSeconds(seconds);
        show_broom.SetActive(false);
    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void ResetGame()
    {
        if (trigger == 0)
        {
            wYellow.transform.SetParent(wCore.transform);
            wRed.transform.SetParent(wCore.transform);
            wBlue.transform.SetParent(wCore.transform);
            wDarkBlue.transform.SetParent(wCore.transform);
            JigSawPuzzleComplete();
        }
    }
}
