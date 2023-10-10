using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Movement variables
    public float jumpForce = 6f, runningSpeed = 8f;
    Rigidbody2D rigidBody;
    public LayerMask groundMask;
    Vector3 startPosition;
    public Text maxScoreText, maxCoinsText;
    public AudioSource start, die;

    public bool startVelocity, alreadyKilled;

    // Start is called before the first frame update

    void Awake() 
    {
        rigidBody = GetComponent<Rigidbody2D>();    
    }
    void Start()
    {
        startPosition = this.transform.position;
        startVelocity = false;
        alreadyKilled = false;
    }

    public void StartGame(){
        Invoke("RestartPosition", 0f); //Nos permite retrasar la llamada a un metodo
    }

    void RestartPosition(){
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;
        runningSpeed = 8f; //8f
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CamaraFollow>().ResetCameraPosition();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") ) 
        {
            Jump();
        }
        Debug.DrawRay(this.transform.position, (Vector2.down + Vector2.right) * new Vector2(0.6f,1.2f), Color.red);
        Debug.DrawRay(this.transform.position, Vector2.down * 1.1f, Color.green);
        Debug.DrawRay(this.transform.position, (Vector2.down + Vector2.left) * new Vector2(0.7f,1.2f), Color.blue);
        
    }

    private void FixedUpdate() {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame){
            if (startVelocity){
                runningSpeed += 0.001f; 
                rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
            } else {
                rigidBody.velocity = new Vector2(Vector2.zero.x, rigidBody.velocity.y);
            }
        } else {
            rigidBody.velocity = new Vector2(Vector2.zero.x, rigidBody.velocity.y);
        }
    }

    void Jump(){
        if (GameManager.sharedInstance.currentGameState == GameState.inGame){
            if (IsTouchingTheGround()){
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    bool IsTouchingTheGround(){
        Vector2 distance = Vector2.down * new Vector2(1f, 1.2f) + Vector2.right * new Vector2(0.6f,1f);
        Vector2 distance2 = Vector2.down * new Vector2(1f, 1.2f) + Vector2.left * new Vector2(0.7f,1f);
        if (
            Physics2D.Raycast( this.transform.position, distance, 1.3f, groundMask) || 
            Physics2D.Raycast( this.transform.position, Vector2.down, 1.1f, groundMask) ||
            Physics2D.Raycast( this.transform.position, distance2, 1.3f, groundMask)){ //||  
            return true;
        } else{
            return false;
        }
    }
    public void Die(){
        die.Play();
        float travelledDistance = PlayerPrefs.GetFloat("score", 0f); 
        int coins = GameManager.sharedInstance.collectedObject;
        
        float previousMaxScore = PlayerPrefs.GetFloat("maxscore", 0f);
        float previousMaxCoins = PlayerPrefs.GetInt("maxcoins", 0);

        if (travelledDistance > previousMaxScore){
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }
        if (coins > previousMaxCoins){
            PlayerPrefs.SetInt("maxcoins", coins );
        }
        PlayerPrefs.SetInt("coins", coins);
        GameManager.sharedInstance.GameOver();
    }

    public float GetTravelledDistance(){
        return this.transform.position.x - startPosition.x;
    }

    public IEnumerator StartRun() {
        yield return new WaitForSeconds(1f);
        startVelocity = true;
    }
}
