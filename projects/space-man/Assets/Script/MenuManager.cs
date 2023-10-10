using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public static MenuManager sharedInstance;
    public Canvas menuCanvas, gameCanvas, gameOverCanvas;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    public void ShowMainMenu() {
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
        gameOverCanvas.enabled = false;
    }

    public void ShowGameMenu() {
        menuCanvas.enabled = false;
        gameCanvas.enabled = true;
        gameOverCanvas.enabled = false;
    }

    public void ShowGameOverMenu() {
        menuCanvas.enabled = false;
        gameCanvas.enabled = false;
        gameOverCanvas.enabled = true;
    }
    


    public void ExitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
