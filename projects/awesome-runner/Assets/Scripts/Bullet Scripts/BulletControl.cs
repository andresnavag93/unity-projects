using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

    public float lifeTime = 5f;
    private float startY;

    void Start() {
        startY = transform.position.y;
    }

    void LateUpdate() {
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
    }

    IEnumerator TurnOffBullet() {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.MONSTER_TAG || other.tag == Tags.PLAYER_TAG ||
            other.tag == Tags.MONSTER_BULLET_TAG || other.tag == Tags.PLAYER_BULLET_TAG) {
            gameObject.SetActive(false);
        }   
    }

} //class
