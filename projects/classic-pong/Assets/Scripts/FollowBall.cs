using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public Transform ball;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        if (ball.GetComponent<BallBehaviour>().gameStarted)
        {
            float yOffset = 0f;
            if(transform.position.y < ball.position.y){
                yOffset = transform.position.y + speed * Time.deltaTime;
                //transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
            } else if (transform.position.y > ball.position.y){
                yOffset = transform.position.y - speed * Time.deltaTime;
                //transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
            } 
            //transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(yOffset, -3.7f, 3.7f), transform.position.z);
        }
    }
}
