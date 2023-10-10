using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rSpeed = 10.0f;
    public float mSpeed = 20.0f;
    public float X = 0.0f;
    public float Y = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // start the game with the cursor locked
        LockCursor(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if ESCAPE key is pressed, then unlock the cursor
        if (Input.GetButtonDown("Cancel"))
        {
            LockCursor(false);
        }

        // if the player fires, then relock the cursor
        if (Input.GetButtonDown("Fire1"))
        {
            LockCursor(true);
        }

        X += Input.GetAxis("Mouse X") * rSpeed;
        Y += -Input.GetAxis("Mouse Y") * rSpeed;
        transform.localRotation = Quaternion.AngleAxis(X, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(Y, Vector3.left);
        transform.position += transform.forward * mSpeed * -Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += transform.right * mSpeed * -Input.GetAxis("Horizontal") * Time.deltaTime;

    }

    private void LockCursor(bool isLocked)
    {
        if (isLocked)
        {
            // make the mouse pointer invisible
            Cursor.visible = false;

            // lock the mouse pointer within the game area
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            // make the mouse pointer visible
            Cursor.visible = true;

            // unlock the mouse pointer so player can click on other windows
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
