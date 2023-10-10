using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            PlayerController controller = other.GetComponent<PlayerController>();
            if (!controller.alreadyKilled){
                controller.alreadyKilled = true;
                controller.Die();
            }
        }   
    }
}
