using UnityEngine;
using System.Collections;

public class MovePuertas : MonoBehaviour {

    public float speed; 
    public float distance;
    private float yStartPosition;
 
    // Use this for initialization
    void Start () {

        yStartPosition = transform.position.y;
    }
   
    // Update is called once per frame
    void Update () {
 
        if (transform.position.y < yStartPosition || transform.position.y > yStartPosition + distance) {
            speed *= -1;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
    }
}  