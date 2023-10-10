using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    //Posicion, variable transform de la paleta referencia
    public Transform paddle;
    public bool gameStarted = false; //Por defecto es falso y hay que hacerla publica para que pueda ser accesida por otro game objects
    public Rigidbody2D rbball;
    float posDif;
    public AudioSource ballKick;
    public AudioSource ballPoint;
    // Start is called before the first frame update
    void Start()
    {
        posDif = paddle.position.x - transform.position.x; //Mantener la posicion de la pelota cerca de la plataforma
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            
            transform.position =  new Vector3(paddle.position.x - posDif, paddle.position.y, paddle.position.z);
            if ( Input.GetMouseButtonDown(0) ) //Funcion para detectar click del mouse el click izquierdo es 0
            {
                rbball.velocity = new Vector2(8,8); //Esto es porque se le direccion y una magnitud
                gameStarted = true;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        ballKick.Play();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        ballPoint.Play();
    }
}
