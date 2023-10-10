using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDamageShoot : MonoBehaviour {

    [SerializeField]
    private Transform playerBullet;
    public GameObject playerDiedEffect;
    private float distanceBeforeNewPlatforms = 120f;
    private LevelGenerator levelGenerator;
    private LevelGeneratoPooling levelGenerato_pooling;

    [HideInInspector]
    public bool canShoot;
    private Button shootBtn;


    void Awake() {
        levelGenerator = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGenerator>();
        levelGenerato_pooling = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGeneratoPooling>();

        shootBtn = GameObject.Find(Tags.SHOOT_BUTTON_OBJ).GetComponent<Button>();
        shootBtn.onClick.AddListener(() => Shoot());
    }

    void Update() {
        Fire();
    }

    void Fire() {
        if (Input.GetKeyDown(KeyCode.K)) {
            if (canShoot) {
                Vector3 bulletPos = transform.position;
                bulletPos.y += 1.5f;
                bulletPos.x += 1f;
                Transform newBullet = (Transform)Instantiate(playerBullet, bulletPos, Quaternion.identity);
                newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1550f);
                newBullet.parent = transform;
            }
        }
    }

    void Shoot() {
        if (canShoot) {
            Vector3 bulletPos = transform.position;
            bulletPos.y += 1.5f;
            bulletPos.x += 1f;
            Transform newBullet = (Transform)Instantiate(playerBullet, bulletPos, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1550f);
            newBullet.parent = transform;
        }
    }

    void PlayerDied() {
        Vector3 effectPos = transform.position;
        effectPos.y += 2f;
        Instantiate(playerDiedEffect, effectPos, Quaternion.identity);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.MONSTER_BULLET_TAG || other.tag == Tags.BOUNDS_TAG) {
            // inform game controller that the player has died
            PlayerDied();
            GameplayController.instance.TakeDamage();
            Destroy(gameObject);
        }

        if (other.tag == Tags.HEALTH_TAG) {
            // inform gameplay controller that we have collected a health
            GameplayController.instance.IncrementHealth();
            other.gameObject.SetActive(false);
        }

        if (other.tag == Tags.MORE_PLATFORMS_TAG) {
            Vector3 temp = other.transform.position;
            temp.x += distanceBeforeNewPlatforms;
            other.transform.position = temp;
            //levelGenerator.GenerateLevel(false);
            levelGenerato_pooling.PoolingPlatforms();
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == Tags.MONSTER_TAG) {
            // inform game controller that the player has died
            PlayerDied();
            GameplayController.instance.TakeDamage();
            Destroy(gameObject);
        }
    }

} //class
