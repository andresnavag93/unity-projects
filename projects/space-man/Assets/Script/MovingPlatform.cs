using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = true;
    }
}
