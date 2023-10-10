using UnityEngine;
using System.Collections;

public class GameObjectAux : MonoBehaviour {

    public float turnSpeed = 5f;  // Rotar nave

    // Nave es un cuerpo rígido
    void Start () {

        
    }

    // Movimientos de la nave
    void Update () {
        
        if (Input.GetKey(KeyCode.LeftArrow)) {

            transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        }
        
        if (Input.GetKey(KeyCode.RightArrow)) {

            transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        }  
    }
}
