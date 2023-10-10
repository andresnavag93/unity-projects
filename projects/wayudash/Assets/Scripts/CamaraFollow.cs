using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform target; //Perseguir a un objetivos
    public Vector3 offset = new Vector3(0.2f, 0.0f, -10f), velocity = Vector3.zero;
    public float dampingTime = 0.3f;

    private void Awake() {
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition(){
        MoveCamera(false);
    }

    void MoveCamera(bool smooth){
        Vector3 destination = new Vector3(
            target.position.x - offset.x,
            offset.y, 
            offset.z
        );

        if (smooth){
            this.transform.position = Vector3.SmoothDamp(
                this.transform.position, 
                destination,
                ref velocity,
                dampingTime
            );
        } else {
            this.transform.position = destination;
        }
    }
}
