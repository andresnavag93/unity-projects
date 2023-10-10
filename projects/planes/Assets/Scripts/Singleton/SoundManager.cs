using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource audioSource;

    public AudioClip shoot;

    private void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    public void PlayShoot() {
        audioSource.PlayOneShot(shoot);
    }
}
