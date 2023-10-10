using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;
    public Canvas menuCanvas, gameCanvas;
    public GameObject loginView, registerView, spanishInstructions, englishInstructions, playMode, instructionMode, statsMode, gameOverMove;
    public List<GameObject> listInstructions = new List<GameObject>(), listInstructionsEnglish = new List<GameObject>();
    public int instruction = 0, language = 1, auth;

    // Start is called before the first frame update
    private void Awake() {
        if (sharedInstance == null){
            sharedInstance = this;
        }         
        PlayerPrefs.SetInt("auth_id", 0);  
    }
    public void ShowLoginMode(){
        auth = PlayerPrefs.GetInt("auth_id", 0);
        if (auth == 0){
            loginView.SetActive(true);
            registerView.SetActive(false);
            statsMode.SetActive(false);
        } else {
            loginView.SetActive(false);
            registerView.SetActive(false);
            statsMode.SetActive(true); 
        }
        playMode.SetActive(false); 
        instructionMode.SetActive(false);
        gameOverMove.SetActive(false);
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    public void ShowRegisterMode(){
        auth = PlayerPrefs.GetInt("auth_id", 0);
        if (auth == 0){
            loginView.SetActive(false);
            registerView.SetActive(true);
            statsMode.SetActive(false);
        } else {
            loginView.SetActive(false);
            registerView.SetActive(false);
            statsMode.SetActive(true);  
        }
        playMode.SetActive(false); 
        instructionMode.SetActive(false);
        gameOverMove.SetActive(false);
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    public void ShowMenuStatsMode(){
        auth = PlayerPrefs.GetInt("auth_id", 0);
        if (auth == 0){
            loginView.SetActive(true);
            registerView.SetActive(false);
            statsMode.SetActive(false);
        } else {
            loginView.SetActive(false);
            registerView.SetActive(false);
            statsMode.SetActive(true);  
        }
        playMode.SetActive(false); 
        gameOverMove.SetActive(false);
        instructionMode.SetActive(false);
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    public void ShowInstructionMode(){
        loginView.SetActive(false);
        registerView.SetActive(false);
        statsMode.SetActive(false); 
        playMode.SetActive(false); 
        gameOverMove.SetActive(false);
        instructionMode.SetActive(true);
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    public void ShowInGame(){
        loginView.SetActive(false);
        registerView.SetActive(false);
        statsMode.SetActive(false); 
        playMode.SetActive(false); 
        gameOverMove.SetActive(false);
        instructionMode.SetActive(false);
        menuCanvas.enabled = false;
        gameCanvas.enabled = true;
    }

    public void ShowMenuPlayMode(){
        loginView.SetActive(false);
        registerView.SetActive(false);
        statsMode.SetActive(false); 
        playMode.SetActive(true); 
        gameOverMove.SetActive(false);
        instructionMode.SetActive(false);
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    public void ShowMenuGameOverMode(){
        loginView.SetActive(false);
        registerView.SetActive(false);
        statsMode.SetActive(false); 
        playMode.SetActive(false); 
        gameOverMove.SetActive(true);
        instructionMode.SetActive(false);
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    public void ShowMenuInstructionRightMode(){
        playMode.SetActive(false); 
        loginView.SetActive(false);
        statsMode.SetActive(false); 
        registerView.SetActive(false);
        gameOverMove.SetActive(false);
        instructionMode.SetActive(true);
        if (instruction < listInstructions.Count-1){
            listInstructions[instruction].SetActive(false);
            listInstructionsEnglish[instruction].SetActive(false);
            instruction += 1;
            listInstructions[instruction].SetActive(true);
            listInstructionsEnglish[instruction].SetActive(true);
        }
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    public void ShowMenuInstructionLeftMode(){
        playMode.SetActive(false); 
        loginView.SetActive(false);
        statsMode.SetActive(false); 
        registerView.SetActive(false);
        gameOverMove.SetActive(false);
        instructionMode.SetActive(true);

        if (instruction > 0){
            listInstructions[instruction].SetActive(false);
            listInstructionsEnglish[instruction].SetActive(false);
            instruction -= 1;
            listInstructions[instruction].SetActive(true);
            listInstructionsEnglish[instruction].SetActive(true);
        }
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    public void ChangeToSpanish(){
        language = 0;
        spanishInstructions.SetActive(true);
        englishInstructions.SetActive(false);
    }

    public void ChangeToEnglish(){
        language = 1;
        spanishInstructions.SetActive(false);
        englishInstructions.SetActive(true);
    }

    public void ExitGame(){
        PlayerPrefs.SetString("auth_token", "");
        PlayerPrefs.SetInt("auth_id", 0);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
