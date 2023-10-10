using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float  runningSpeed = 1.5f;
    Rigidbody2D rigidBody;
    public bool facingRight = false;
    private Vector3 startPosition;

    public int enemyDamage = 10;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float currentRunningSpeed = runningSpeed;
        if (facingRight)
        {
            currentRunningSpeed = runningSpeed;
            this.transform.eulerAngles = new Vector3(0,180,0);
        } else {
            currentRunningSpeed = -runningSpeed;
            this.transform.eulerAngles = Vector3.zero;
        }

        if (GameManager.sharedInstance.currentGameState == GameState.inGame) {
            rigidBody.velocity = new Vector2(currentRunningSpeed, rigidBody.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Coin" || other.tag == "ExitZone")
        {
            return;
        }

        if(other.tag == "Player"){
            other.gameObject.GetComponent<PlayerController>().
                     CollectHealth(-enemyDamage);
            return;
        }

        //Si llegamos aquí, no hemos chocado ni con monedas, ni con players
        //Lo más normal es que aquí haya otro enemigo o bien escenario
        //Vamos a hacer que el enemigo rote
        facingRight = !facingRight;
    }
}
