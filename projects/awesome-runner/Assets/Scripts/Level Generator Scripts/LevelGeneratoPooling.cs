﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratoPooling : MonoBehaviour {

    [SerializeField]
    private Transform platform, platformParent;

    [SerializeField]
    private Transform monster, monsterParent;

    [SerializeField]
    private Transform heatlthCollectable, healthCollectableParent;

    [SerializeField]
    private int levellength = 100;

    [SerializeField]
    private float distanceBetweenPlatforms = 15f;

    [SerializeField]
    private float MinPositionY = 0f, MaxPositionY = 7f;

    [SerializeField]
    private float chanceForMonsterExistence = 0.25f, chanceForHealthCollectableExistence = 0.1f;

    [SerializeField]
    private float healthCollectableMinY = 1f, healthCollectableMaxY = 3f;

    private float platformLastPositionX;
    private Transform[] platformArray;


    // Start is called before the first frame update
    void Start() {
        CreatePlatforms();
    }


    void CreatePlatforms() {
        platformArray = new Transform[levellength];
        for (int i = 0; i < platformArray.Length; i++) {
            Transform newPlatform = (Transform)Instantiate(platform, Vector3.zero, Quaternion.identity);
            platformArray[i] = newPlatform;

        }

        for (int i = 0; i < platformArray.Length; i++) {
            float platformPositionY = Random.Range(MinPositionY, MaxPositionY);

            Vector3 platformPosition;

            if (i < 2) {
                platformPositionY = 0f;
            }

            platformPosition = new Vector3(distanceBetweenPlatforms * i, platformPositionY, 0);
            platformLastPositionX = platformPosition.x;

            platformArray[i].position = platformPosition;
            platformArray[i].parent = platformParent;

            //spawn monsters and health collectables
            SpawnHealthAndMonster(platformPosition, i, true);
        }
    }

    public void PoolingPlatforms() {
        for (int i = 0; i < platformArray.Length; i++) {
            if (!platformArray[i].gameObject.activeInHierarchy) {
                platformArray[i].gameObject.SetActive(true);
                float platformPositionY = Random.Range(MinPositionY, MaxPositionY);
                Vector3 platformPosition = new Vector3(distanceBetweenPlatforms + platformLastPositionX,
                    platformPositionY, 0);

                platformArray[i].position = platformPosition;
                platformLastPositionX = platformPosition.x;

                // spawn health and monsters
                SpawnHealthAndMonster(platformPosition, i, false);
            }
        }
    }

    void SpawnHealthAndMonster(Vector3 platformPosition, int i, bool gameStarted) {
        if (i > 2) {
            if (Random.Range(0f,1f) < chanceForMonsterExistence) {
                if (gameStarted) {
                    platformPosition = new Vector3(distanceBetweenPlatforms * i,
                        platformPosition.y + 0.1f, 0);
                } else {
                    platformPosition = new Vector3(distanceBetweenPlatforms + platformLastPositionX,
                        platformPosition.y + 0.1f, 0);
                }

                Transform createMonster = (Transform)Instantiate(monster, platformPosition,
                    Quaternion.Euler(0, -90, 0));
                createMonster.parent = monsterParent;
            } // if for monster

            if (Random.Range(0f, 1f) < chanceForHealthCollectableExistence) {
                if (gameStarted) {
                    platformPosition = new Vector3(distanceBetweenPlatforms * i,
                        platformPosition.y + Random.Range(healthCollectableMinY, healthCollectableMaxY), 0);
                } else {
                    platformPosition = new Vector3(distanceBetweenPlatforms + platformLastPositionX,
                        platformPosition.y + Random.Range(healthCollectableMinY, healthCollectableMaxY), 0);
                }

                Transform createHealthCollectable = (Transform)Instantiate(heatlthCollectable, platformPosition,
                    Quaternion.identity);
                createHealthCollectable.parent = healthCollectableParent;
            } // if for health collectables
        }        
    }

} // class
