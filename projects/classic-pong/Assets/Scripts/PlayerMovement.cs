using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //Debug.Log(Input.mousePosition);  
      // transform.position = Input.mousePosition; 
      // Traducir nuestras posicion en pixel (pantalla) en el mundo (en escena)
      Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
      // Usamos las posiciones en z del padel y las posiciones x, y del mouse
      //transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
      //Ahora solo queda setear la posicion del eje x fija y que solo se mueva en y
      //transform.position = new Vector3(transform.position.x, mousePos.y, transform.position.z);
      //Limites del eje y
      transform.position = new Vector3(transform.position.x,Mathf.Clamp(mousePos.y, -3.8f, 3.8f), transform.position.z);
    }
}
