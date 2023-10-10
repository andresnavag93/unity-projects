using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{

    public Text scoreText, coinsText;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame){
            int coins = GameManager.sharedInstance.collectedObject;
            float score = controller.GetTravelledDistance();
            scoreText.text = "Score: "+ score.ToString("f0");
            coinsText.text = "Coins: "+ coins.ToString();
            PlayerPrefs.SetFloat("score", score);
            PlayerPrefs.SetString("realscore", score.ToString("f0"));
        }    
    }
}
