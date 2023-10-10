using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {   
            LevelManager.sharedInstance2.AddCustomLevelBlock();
            LevelManager.sharedInstance2.RemoveLevelBlock();
        }
    }
}
