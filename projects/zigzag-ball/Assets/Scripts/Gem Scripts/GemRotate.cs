using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemRotate : MonoBehaviour
{

    private float speed = 1f;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        angle = (angle + speed) % 360f;
        transform.localRotation = Quaternion.Euler(new Vector3(45f, angle, 0f));
    }

}//class
