using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void PlayerDeath();
    public static event PlayerDeath OnPlayerDeath;

    public GameObject GameOverScreen;

    public static Action OnUpdateScore;

    private void OnEnable() {
        OnUpdateScore += UpdateScoreUI;
    }

    private void Awake()
    {
        GameOverScreen.SetActive(false);
        OnPlayerDeath += ShowGameOverScreen;
    }

    private void ShowGameOverScreen()
    {
        GameOverScreen.SetActive(true);
    }

    public void PlayerKilled()
    {
        OnPlayerDeath?.Invoke();
    }

    public void UpdateScoreUI() {
        // Change value UI score
        Debug.Log("Score Actualizado");
    }

}