using UnityEngine;
using System.Collections;

public class GameObjectCamara : MonoBehaviour {

    public Rigidbody rb; // Nave es un cuerpo rígido
    public float moveSpeed = 50f; // Mover nave
    public float maxMoveSpeed = 25f; // Maximo de velocidad de la nave
    
    // Nave es un cuerpo rígido
    void Start () {

        rb = GetComponent<Rigidbody>();
    }

    // Movimientos de la nave
    void Update () {

        if (Input.GetKey(KeyCode.UpArrow)) {

            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow)) {

        	transform.Translate(-Vector3.left * moveSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.RightArrow)) {

        	transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.DownArrow)) {

            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W)) {

            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S)) {

            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

    }

    // Bajar velocidad al colisionar con otros objetos que no son monedas
    void FixedUpdate() {

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Para acelerar la nave
        if (Input.GetKey(KeyCode.A)) {

            if (moveSpeed < maxMoveSpeed) {
                moveSpeed += 5f;
            }
        }
    }

    // Colision con los obstaculos en el camino
    void OnTriggerEnter(Collider col) {

        if (col.gameObject.CompareTag ("Pick Up")) {
            col.gameObject.SetActive (false);
        }

        if (col.gameObject.CompareTag ("Pinguino")) {
            moveSpeed -= 10f;
        }

        if (col.gameObject.CompareTag ("Gusano")) {
            moveSpeed -= 10f;
        }

        if (col.gameObject.CompareTag ("Dragon")) {
            moveSpeed -= 10f;
        }

        if (col.gameObject.CompareTag ("Finish")) {
            Application.Quit();
        }
    }
}
