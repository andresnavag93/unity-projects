using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed = .5f;
    public float jumpspeed = 300;
    public bool isGrounded = true;
    bool isAire = false;
    public Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   //Input.GetAxis("Horizontal") Devuelve un numero que indica la direccion del movimiento 
        playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerRb.velocity.y);
        
        if( Input.GetAxis("Horizontal") == 0) 
        {
            playerAnim.SetBool("isWalking", false);
        } else if (Input.GetAxis("Horizontal") < 0 ){
            GetComponent<SpriteRenderer>().flipX = true;
            playerAnim.SetBool("isWalking", true);
        } else if (Input.GetAxis("Horizontal") > 0 ){
            GetComponent<SpriteRenderer>().flipX = false;
            playerAnim.SetBool("isWalking", true);
        }
        if(isGrounded){
            if (Input.GetKeyDown(KeyCode.Space))
            {

                GetComponent<AudioSource>().Play();
                //Debug.Log("Entra");
                playerRb.AddForce(Vector2.up * jumpspeed); //Otra manera de agregar movimiento en este caso hacia arriba    
                isGrounded = false;
            }
        } else if (!isGrounded){
            if (!isAire){
                playerAnim.SetTrigger("JumpTrigger");
                isAire = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //Cunado toque la collision respectiva del piso
        if(other.gameObject.CompareTag("Ground")){
            isGrounded = true;
            isAire = false;
            //playerAnim.SetTrigger("JumpTrigger");
        }
    }
}
