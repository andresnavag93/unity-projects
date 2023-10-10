using UnityEngine;

/// <summary>
/// Ui Manager
/// </summary>
public class UIManager : MonoBehaviour
{
    public Rigidbody2D playerRigidbody; 
    public PlayerMovement playerMovement;
    public GameObject[] buttons;

    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android || 
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
        }
    }

    public void OnPressUIButtonUp()
    {
        playerMovement.isOnUiButtonPress = true;
        playerMovement.movement = Vector2.up * playerMovement.speed;
    }

    public void OnPressUIButtonDown()
    {
        playerMovement.isOnUiButtonPress = true;
        playerMovement.movement = Vector2.down * playerMovement.speed;
    }

    public void OnPressUIButtonRight()
    {
        playerMovement.isOnUiButtonPress = true;
        playerMovement.movement = Vector2.right * playerMovement.speed;
    }

    public void OnPressUIButtonLeft()
    {
        playerMovement.isOnUiButtonPress = true;
        playerMovement.movement = Vector2.left * playerMovement.speed;
    }

    public void OnRelease()
    {
        playerMovement.isOnUiButtonPress = false;
        playerMovement.movement = Vector2.zero;
    }

    public void ExitApp()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
