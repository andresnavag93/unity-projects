using UnityEngine;

/// <summary>
/// Camera Follow
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0.0f, 0.0f, -10f);
    public float dampingTime = 0.3f;
    public Vector3 velocity = Vector3.zero;

    #region Mono
    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        MoveCamera(true);
    }

    #endregion

    #region Functions
    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }

    void MoveCamera(bool smooth)
    {
        Vector3 destination = new Vector3(
            target.position.x - offset.x,
            target.position.y - offset.y,
            offset.z
        );
        if (smooth)
        {
            this.transform.position = Vector3.SmoothDamp(
                this.transform.position,
                destination,
                ref velocity, dampingTime
            );
        }
        else
        {
            this.transform.position = destination;
        }
    }
    #endregion
}
