using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    public GameObject monsterDiedEffect;
    public Transform bullet;
    public float distanceFromPlayerToStartMove = 20f;
    public float movementSpeedMin = 1f;
    public float movementSpeedMax = 2f;
    private bool moveRight;
    private float movementSpeed;
    private bool isPLayerInRegion;
    private Transform playerTransform;
    public bool canShoot;
    private string FUNCTION_TO_INVOKE = "StartShooting";

    void Start(){
        if (Random.Range(0.0f, 1.0f) > 0.5f) {
            moveRight = true;
        } else {
            moveRight = false;
        }

        movementSpeed = Random.Range(movementSpeedMin, movementSpeedMax);
        playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    void Update() {
        if (playerTransform) {
            float distanceFromPlayer = (playerTransform.position - transform.position).magnitude;

            if (distanceFromPlayer < distanceFromPlayerToStartMove) {
                if (moveRight) {
                    Vector3 temp = transform.position;
                    temp.x += Time.deltaTime * movementSpeed;
                    transform.position = temp;
                    //transform.position = new Vector3(transform.position.x+Time.deltaTime*movementSpeed, transform.position.y, transform.position.z)
                } else {
                    Vector3 temp = transform.position;
                    temp.x -= Time.deltaTime * movementSpeed;
                    transform.position = temp;
                }

                if (!isPLayerInRegion) {
                    if (canShoot) {
                        InvokeRepeating(FUNCTION_TO_INVOKE, 0.5f, 1.5f);
                    }
                    isPLayerInRegion = true;
                }
            } else {
                CancelInvoke(FUNCTION_TO_INVOKE);
            }
        }
    }

    void StartShooting() {
        if (playerTransform) {
            Vector3 bulletPos = transform.position;
            bulletPos.y += 1.5f;
            bulletPos.x -= 1f;
            Transform newBullet = (Transform)Instantiate(bullet, bulletPos, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
            newBullet.parent = transform;
        }
    }

    void MonsterDied() {
        Vector3 effectPos = transform.position;
        effectPos.y += 2f;
        Instantiate(monsterDiedEffect, effectPos, Quaternion.identity);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLAYER_BULLET_TAG) {
            GameplayController.instance.IncrementScore(200);
            MonsterDied();
        }  
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == Tags.PLAYER_TAG) {
            MonsterDied();
        }
    }

} // class
