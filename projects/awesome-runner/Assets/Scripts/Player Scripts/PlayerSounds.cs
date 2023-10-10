﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    private float collisionSoundEffect = 1f;
    public float audioFootVolume = 1f;
    public float soundEffectPitchRandomess = 0.05f;

    private AudioSource audioSource;
    public AudioClip genericFootSound;
    public AudioClip metalFootSound;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void FootSound() {
        audioSource.volume = collisionSoundEffect * audioFootVolume;
        audioSource.pitch = Random.Range(1.0f - soundEffectPitchRandomess,
            1.0f - soundEffectPitchRandomess);

        if (Random.Range(0, 2) > 0) {
            audioSource.clip = genericFootSound;
        } else {
            audioSource.clip = metalFootSound;
        }
        audioSource.Play();
    }

} // class
