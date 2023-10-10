using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed = 5f;
    public float jumpPower = 10f;
    public float secondJumpPower = 10f;
    public float radius = 0.3f;

    private bool isGrounded; //is we are standing by ground
    private bool playerJumped = false;
    private bool canDoubleJump = false;
    private bool gameStarted;

    private Rigidbody myBody;
    private PlayerAnimation playerAnim;
    private BGScroller bgScroller;
    private PlayerHealthDamageShoot playerShoot;
    private Button jumpBtn;

    public Transform groundCheckPosition;
    public LayerMask layerGround;
    public GameObject smokePosition;

    void Awake() {
        myBody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnimation>();
        bgScroller = GameObject.Find(Tags.BACKGROUND_GAME_OBJ).GetComponent<BGScroller>();
        playerShoot = GetComponent<PlayerHealthDamageShoot>();
        jumpBtn = GameObject.Find(Tags.JUMP_BUTTON_OBJ).GetComponent<Button>();
        jumpBtn.onClick.AddListener(() => Jump());
    }

    void Start() {
        StartCoroutine(StartGame());
    }

    void Update() {
        if (gameStarted) {
            PlayerJump();
        }
    }

    void FixedUpdate() {
        if (gameStarted) {
            PlayerMove();
            PlayerGrounded();
        }
    }

    void PlayerMove() {
        myBody.velocity = new Vector3(movementSpeed, myBody.velocity.y, 0f);
    }

    void PlayerGrounded() {
        isGrounded = Physics.OverlapSphere(groundCheckPosition.position,
            radius, layerGround).Length > 0;
    }

    void PlayerJump() {
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canDoubleJump) {
            canDoubleJump = false;
            myBody.AddForce(new Vector3(0, secondJumpPower, 0));
        } else if (Input.GetKeyUp(KeyCode.Space) && isGrounded) {
            playerAnim.DidJump();
            myBody.AddForce(new Vector3(0, jumpPower, 0));
            playerJumped = true;
            canDoubleJump = true;
        }
    }

    public void Jump() {
        if (!isGrounded && canDoubleJump) {
            canDoubleJump = false;
            myBody.AddForce(new Vector3(0, secondJumpPower, 0));
        } else if (isGrounded) {
            playerAnim.DidJump();
            myBody.AddForce(new Vector3(0, jumpPower, 0));
            playerJumped = true;
            canDoubleJump = true;
        }
    }


    IEnumerator StartGame() {
        yield return new WaitForSeconds(0f);
        gameStarted = true;
        bgScroller.canScroll = true;
        playerShoot.canShoot = true;
        GameplayController.instance.canCountScore = true;
        smokePosition.SetActive(true);
        playerAnim.PlayerRun();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == Tags.PLATFORM_TAG) {
            if (playerJumped) {
                playerJumped = false;
                playerAnim.DidLand();
            }
        }
    }

} // class
