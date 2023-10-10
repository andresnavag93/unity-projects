using UnityEngine;
using System.Collections;

public class Nave : MonoBehaviour {

    public float turnSpeed = 5f;  // Rotar nave
    //public float speed = 1.0f;


    // Nave es un cuerpo rígido
    void Start () {

       // transform.eulerAngles = transform.position;
    }

    // Movimientos de la nave
    void Update () {
        
        if (Input.GetKey(KeyCode.LeftArrow)) {

        	transform.Rotate(-Vector3.right, 25 * turnSpeed * Time.deltaTime);
            transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        }
        
        if (Input.GetKey(KeyCode.RightArrow)) {

            transform.Rotate(Vector3.left, 25 * -turnSpeed * Time.deltaTime);
            transform.RotateAround(transform.position, -transform.up, Time.deltaTime * 90f);
        }

        /*else {

            transform.eulerAngles = Vector3.Lerp(transform.position, transform.position * 1f, Time.deltaTime * speed);
        }*/
    }
}