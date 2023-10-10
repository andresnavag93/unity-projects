using UnityEngine;
using System.Collections;

public class Moneda : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(200 * Time.deltaTime, 0, 0); //rotates 50 degrees per second around z axis
    }
}