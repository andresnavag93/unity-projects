using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Update is called once per frame
    public float degreesPerSecond = 20;

    public float rotateX = 0;
    public float rotateY = 0;
    public float rotateZ = 0;
    private void Update()
    {
        transform.Rotate(new Vector3(
            rotateX * degreesPerSecond, 
            rotateY * degreesPerSecond, 
            rotateZ * degreesPerSecond) * Time.deltaTime);
    }
}
