using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Permite incluir todos los elementos de UI Texto, Botones ETc.
using UnityEngine.SceneManagement; //Permite cambiar de escena, reiniciarla, etc..

public class DeadZone : MonoBehaviour
{
    public Text scorePlayerText;
    public Text scoreEnemyText;
    int scorePlayerQuantity;
    int scoreEnemyQuantity;
    public SceneChanger sceneChanger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Collision == Choque
    //private void OnCollisionEnter2D(Collision2D other) { //other es el objeto que esta chocando
    //    Debug.Log("Colision");
    //}

    // Trigger = Atraviesa algo
    private void OnTriggerEnter2D(Collider2D other) {  //other es el objeto que esta atravesando es como un disparador
       // Debug.Log(gameObject.tag);
        if ( gameObject.CompareTag("Right") )
        {
            scorePlayerQuantity++;
            UpdateScoreLabel(scorePlayerText, scorePlayerQuantity );
        } 
        else if ( gameObject.CompareTag("Left") )
        {
            scoreEnemyQuantity++;
            UpdateScoreLabel(scoreEnemyText, scoreEnemyQuantity );
        }

        other.GetComponent<BallBehaviour>().gameStarted = false;
        CheckScore();
    }

    void CheckScore()
    {
        if (scorePlayerQuantity >= 3)
        {
            sceneChanger.ChangeSceneTo("WinScene");
        }else if (scoreEnemyQuantity >= 3)
        {
            sceneChanger.ChangeSceneTo("LoseScene");
        }
    }

    void UpdateScoreLabel(Text label, int score)
    {
        label.text = score.ToString();
    }
    
}
