using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 6f;
    public float runningSpeed = 6f;

    [SerializeField]
    float raycastLong = 1.5f;

    Rigidbody2D rigidBody;
    public LayerMask groundMask;
    Animator animator;
    Vector3 startPosition;

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const string STATE_RUNNING = "isRunning";

    [SerializeField]
    private int healthPoints, manaPoints;
    public const int INITIAL_HEALTH = 200, INITIAL_MANA = 15,
        MAX_HEALTH = 200, MAX_MANA = 30,
        MIN_HEALTH = 10, MIN_MANA = 0;

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.5f;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }

    public void StartGame()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        animator.SetBool(STATE_RUNNING, false);
        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;
        Invoke("RestartPosition", 0.1f);
    }

    void RestartPosition()
    {
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;
        runningSpeed = 4f;

        GameObject camera = GameObject.Find("Main Camera");
        camera.GetComponent<CameraFollow>().ResetCameraPosition();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump(false);
        }

        if (Input.GetButtonDown("Superjump"))
        {
            Jump(true);
        }

        animator.SetBool(STATE_ON_THE_GROUND, isTouchingTheGround());
        Debug.DrawRay(this.transform.position, Vector2.down * raycastLong, Color.green);
    }

    void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            move();
        }
        else
        { // If we are not in game
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
        //moveWithKey();
    }

    void move()
    {

        if (rigidBody.velocity.x < runningSpeed)
        {
            animator.SetBool(STATE_RUNNING, true);
            rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
        }
    }

    void moveWithKey()
    {

        if (rigidBody.velocity.x == 0 && isTouchingTheGround())
        {
            animator.SetBool(STATE_RUNNING, false);
        }

        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * runningSpeed, rigidBody.velocity.y);

        if (Input.GetAxis("Horizontal") < 0)
        {
            animator.SetBool(STATE_RUNNING, true);
            GetComponent<SpriteRenderer>().flipX = true;
        }

        else if (Input.GetAxis("Horizontal") > 0)
        {
            animator.SetBool(STATE_RUNNING, true);
            GetComponent<SpriteRenderer>().flipX = false;
        }

    }

    void Jump(bool superjump)
    {
        float jumpForceFactor = jumpForce;

        if (superjump && manaPoints >= SUPERJUMP_COST)
        {
            manaPoints -= SUPERJUMP_COST;
            jumpForceFactor *= SUPERJUMP_FORCE;
        }
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (isTouchingTheGround())
            {
                GetComponent<AudioSource>().Play();
                rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
            }
        }
    }

    bool isTouchingTheGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, raycastLong, groundMask))
        {
            // animator.enabled = true;
            return true;
        }
        else
        {
            // animator.enabled = false;
            return false;
        }
    }

    public void Die()
    {

        float travelledDistance = GetTravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0f);
        if (travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }
        animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }

    public void CollectHealth(int points)
    {
        this.healthPoints += points;
        if (this.healthPoints >= MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }

        if (this.healthPoints <= 0){
            Die();
        }
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if (this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        return healthPoints;
    }

    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelledDistance()
    {
        return this.transform.position.x - startPosition.x;
    }
}
