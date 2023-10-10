using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHealPickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // Only heal is collide with the player
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.ReceiveHealing(playerHealth.maximumHealth);
            Destroy(this.gameObject);
        }
    }
}
