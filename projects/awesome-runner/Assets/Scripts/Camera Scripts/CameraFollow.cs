﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform playerTarget;
    public float offsetZ = -15f; 
    public float offsetX = -5f;
    public float constantY = 5f;
    public float cameraLerpTime = 0.05f;

    void Awake() {
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }


    void FixedUpdate() {
        if (playerTarget) { //Here we notice is the player is death or no
            Vector3 targetPosition = new Vector3(playerTarget.position.x + offsetX,
                constantY, playerTarget.position.z + offsetZ);
            transform.position = Vector3.Lerp(transform.position, targetPosition,
                cameraLerpTime);
        }
    }

} // class
