using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
  menu, inGame, gameOver
}

public class GameManager : MonoBehaviour
{
  public GameState currentGameState = GameState.menu;
  public static GameManager sharedInstance;
  private PlayerController controller;
  public int collectedObject = 0;
  void Awake()
  {
    if (sharedInstance == null)
    {
      sharedInstance = this;
    }
    currentGameState = GameState.menu;
  }

  void Start()
  {
    controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      PlayerPrefs.SetString("auth_token", "");
      PlayerPrefs.SetInt("auth_id", 0);
      Application.Quit();
    }
  }

  public void StartGame()
  {
    SetGameState(GameState.inGame);
  }

  public void GameOver()
  {
    SetGameState(GameState.gameOver);
  }

  public void BackToMenu()
  {
    SetGameState(GameState.menu);
  }

  private void SetGameState(GameState newGameState)
  {
    if (newGameState == GameState.menu)
    {
      MenuManager.sharedInstance.ShowLoginMode();
    }
    else if (newGameState == GameState.inGame)
    {
      MenuManager.sharedInstance.ShowInGame();
      LevelManager.sharedInstance2.RemoveAllBlocks();
      LevelManager.sharedInstance2.GenerateCustomInitialBlocks();
      GameManager.sharedInstance.collectedObject = 0;
      controller.startVelocity = false;
      controller.alreadyKilled = false;
      controller.StartGame();
      StartCoroutine(controller.StartRun());

    }
    else if (newGameState == GameState.gameOver)
    {
      MenuManager.sharedInstance.ShowMenuGameOverMode();
      UserModel user = new UserModel();
      user.max_score = PlayerPrefs.GetFloat("score", 0f);
      string realscore = PlayerPrefs.GetString("realscore", "");
      user.max_score = int.Parse(realscore);
      user.max_coins = PlayerPrefs.GetInt("coins", 0);
      int id = PlayerPrefs.GetInt("auth_id", 0);
      if (id != 0)
      {
        // HttpRequestManager.sharedInstance.PutUpdateNewUserScore(user);
      }
      else
      {
        InputFieldManager.sharedInstance.loadingScore.SetActive(false);
      }
    }
    this.currentGameState = newGameState;
  }

  public void CollectObject(Collectable collectable)
  {
    collectedObject += collectable.value;
  }

}
