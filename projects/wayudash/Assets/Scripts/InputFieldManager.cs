using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class InputFieldManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static InputFieldManager sharedInstance;
    // LoginInput / RecoveryInput
    public InputField loginUsername, loginPassword, registerUsername, 
        registerPassword, registerEmail, registerUsernameConfirm, 
        registerPasswordConfirm, registerEmailConfirm;
    public GameObject error, errorLogin, errorMessage, successEmail;
    // RegisterInput 
    public Text instagram_user, max_coins, max_scores, ranking, email, first_place, second_place, third_place, fourh_place, fifth_place;
    public Text first_place_max_coins, second_place_max_coins, third_place_max_coins, fourh_place_max_coins, fifth_place_max_coins;
    public Text first_place_max_score, second_place_max_score, third_place_max_score, fourh_place_max_score, fifth_place_max_score;
    public GameObject loadingLogin, loadingRegister, loadingScore;
    public Button playButton, statsButton, instructionButton, loginButton, registerButton, resetButton, loginLink, registerLink;
    public Toggle toggleButton;
    public GameObject panelReset;
    void Awake() 
    {
        if (sharedInstance == null){
            sharedInstance = this;
        } 
        loginUsername.text = PlayerPrefs.GetString("remember_username", "");
        loginPassword.text = PlayerPrefs.GetString("remember_password", "");  

        if (loginUsername.text != ""){
            toggleButton.isOn = true;
        } else {
            toggleButton.isOn = false;
        }
    }
    
    public void RequestLogin(){
        if ((loginUsername.text == "") || loginPassword.text == ""){
            errorLogin.GetComponent<Text>().text = "YOUR USERNAME OR PASSWORD HAS NOT BEEN ENTERED";
            errorLogin.SetActive(true);
        } else if ((loginUsername.text.Length < 6) || (loginPassword.text.Length < 6) ){
            errorLogin.GetComponent<Text>().text = "ALL FIELDS MUST HAVE AT LEAST 6 CHARACTERS";
            errorLogin.SetActive(true);
        } else if (!IsValidText(loginPassword.text)){
            errorLogin.GetComponent<Text>().text = "ONLY NUMBERS OR LETTERS FOR PASSWORDS FIELDS";
            errorLogin.SetActive(true); 
        } else if (!IsValidUsername(loginUsername.text) ){
            errorLogin.GetComponent<Text>().text = "ONLY NUMBERS, LETTERS, DOT OR UNDERSCORE FOR USERNAMES FIELDS";
            errorLogin.SetActive(true); 
        } else {
            UserModel user = new UserModel();
            user.instagram_user = loginUsername.text;
            user.password = loginPassword.text;
            if (toggleButton.isOn){
                PlayerPrefs.SetString("remember_username", loginUsername.text);
                PlayerPrefs.SetString("remember_password", loginPassword.text);
            }
            // HttpRequestManager.sharedInstance.PostLoginWithUsername(user);
        }
    }
    public void RequestRegister(){
       if ((registerUsername.text == "") || (registerPassword.text  == "") || (registerEmail.text  == "")
        || (registerUsernameConfirm.text == "") || (registerPasswordConfirm.text  == "") || (registerEmailConfirm.text  == "")) {
            errorMessage.GetComponent<Text>().text = "MISSING FIELDS TO FILL";
            errorMessage.SetActive(true);   
        } else if ((registerUsername.text.Length < 6) || (registerPassword.text.Length < 6) || (registerEmail.text.Length < 6)
        || (registerUsernameConfirm.text.Length < 6) || (registerPasswordConfirm.text.Length < 6) || (registerEmailConfirm.text.Length < 6)) {
            errorMessage.GetComponent<Text>().text = "ALL FIELDS MUST HAVE AT LEAST 6 CHARACTERS";
            errorMessage.SetActive(true); 
        } else if ((!IsValidUsername(registerUsername.text)) || (!IsValidUsername(registerUsernameConfirm.text))){
            errorMessage.GetComponent<Text>().text = "ONLY NUMBERS, LETTERS, DOT OR UNDERSCORE FOR USERNAMES FIELDS";
            errorMessage.SetActive(true); 
        } else if ((!IsValidText(registerPassword.text)) || (!IsValidText(registerPasswordConfirm.text))){
            errorMessage.GetComponent<Text>().text = "ONLY NUMBERS OR LETTERS FOR PASSWORDS FIELDS";
            errorMessage.SetActive(true); 
        } else if (!registerUsername.text.Equals(registerUsernameConfirm.text)){
            errorMessage.GetComponent<Text>().text = "THE USERNAMES DON'T MATCH";
            errorMessage.SetActive(true);
        } else if (!registerPassword.text.Equals(registerPasswordConfirm.text)){
            errorMessage.GetComponent<Text>().text = "THE PASSWORDS DON'T MATCH";
            errorMessage.SetActive(true);
        } else if (!IsValidEmail(registerEmail.text) || !IsValidEmail(registerEmailConfirm.text)){
            errorMessage.GetComponent<Text>().text = "INVALID EMAIL FORMAT";
            errorMessage.SetActive(true);
        } else if ((!registerEmail.text.Equals(registerEmailConfirm.text))){
            errorMessage.GetComponent<Text>().text = "THE EMAILS DON'T MATCH";
            errorMessage.SetActive(true);
        }
        else {
            UserModel user = new UserModel();
            user.instagram_user = registerUsername.text;
            user.password = registerPassword.text;
            user.email = registerEmail.text;
            user.instagram_user_confirm = registerUsernameConfirm.text;
            user.password_confirm = registerPasswordConfirm.text;
            user.email_confirm = registerEmailConfirm.text;
            // HttpRequestManager.sharedInstance.PostCreateNewUser(user);
        }

    }
    public void OpenResetPasswordPanel(){
       if (loginUsername.text == ""){
            errorLogin.GetComponent<Text>().text = "NO USERNAME HAS ENTERED";
            errorLogin.SetActive(true);
        } else if (loginUsername.text.Length < 6) {
            errorLogin.GetComponent<Text>().text = "ALL FIELDS MUST HAVE AT LEAST 6 CHARACTERS";
            errorLogin.SetActive(true);
        } else {
            panelReset.SetActive(true);
            // playButton.interactable = false;
            // statsButton.interactable = false;
            // instructionButton.interactable = false;
            //UserModel user = new UserModel();
            //user.instagram_user = loginUsername.text;
            //HttpRequestManager.sharedInstance.PostRecoveryPasswordUser(user);
        }
    }

    public void CloseResetPasswordPanel(){
        panelReset.SetActive(false);
        // playButton.interactable = true;
        // statsButton.interactable = true;
        // instructionButton.interactable = true;
    }

    public void RequestResetPassword(){
        panelReset.SetActive(false);
        if (loginUsername.text == ""){
            errorLogin.GetComponent<Text>().text = "NO USERNAME HAS ENTERED";
            errorLogin.SetActive(true);
        } else if (loginUsername.text.Length < 6) {
            errorLogin.GetComponent<Text>().text = "ALL FIELDS MUST HAVE AT LEAST 6 CHARACTERS";
            errorLogin.SetActive(true);
        } else {
            UserModel user = new UserModel();
            user.instagram_user = loginUsername.text;
            // HttpRequestManager.sharedInstance.PostRecoveryPasswordUser(user);
            // playButton.interactable = true;
            // statsButton.interactable = true;
            // instructionButton.interactable = true;
        }
    }

    public GameObject ActiveInputFieldButton(int value){
        if ((value == 0) || (value == 2)){
            loginUsername.interactable = false;
            loginPassword.interactable = false;
            loginButton.interactable = false;
            registerLink.interactable = false;
            resetButton.interactable= false;
            loadingLogin.SetActive(true);
            error = errorLogin;
        } else if (value == 1) {
            registerUsername.interactable = false;
            registerPassword.interactable = false;
            registerEmail.interactable = false;
            registerUsernameConfirm.interactable = false;
            registerPasswordConfirm.interactable = false;
            registerEmailConfirm.interactable = false;
            registerButton.interactable = false;
            loginLink.interactable = false;
            loadingRegister.SetActive(true);
            error = errorMessage;
        }
        // } else if (value == 2) {
        //     loginUsername.interactable = false;
        //     loginPassword.interactable = false;
        //     loginButton.interactable = false;
        //     registerLink.interactable = false;
        //     resetButton.interactable= false;
        //     loadingLogin.SetActive(true);
        //     error = errorLogin;
        // }
        return error;
    }

    public void UpdateScores(UserModel user){
        instagram_user.text = user.instagram_user;
        max_coins.text = user.max_coins.ToString();
        max_scores.text = user.max_score.ToString();
        ranking.text = user.ranking.ToString();
        email.text = user.email;
        first_place.text = user.first_place;
        second_place.text = user.second_place;
        third_place.text = user.third_place;
        fourh_place.text = user.fourth_place;
        fifth_place.text = user.fifth_place;

        first_place_max_coins.text = user.first_place_max_coins;
        second_place_max_coins.text = user.second_place_max_coins;
        third_place_max_coins.text = user.third_place_max_coins;
        fourh_place_max_coins.text = user.fourth_place_max_coins;
        fifth_place_max_coins.text = user.fifth_place_max_coins;

        first_place_max_score.text = user.first_place_max_score;
        second_place_max_score.text = user.second_place_max_score;
        third_place_max_score.text = user.third_place_max_score;
        fourh_place_max_score.text = user.fourth_place_max_score;
        fifth_place_max_score.text = user.fifth_place_max_score;
        
    }

    public void InActiveInputFieldButton(int value){
        if ((value == 0) || (value == 2)){
            loginUsername.interactable = true;
            loginPassword.interactable = true;
            loginButton.interactable = true;
            registerLink.interactable = true;
            resetButton.interactable= true;
            loadingLogin.SetActive(false);
        } else if (value == 1) {
            registerUsername.interactable = true;
            registerPassword.interactable = true;
            registerEmail.interactable = true;
            registerUsernameConfirm.interactable = true;
            registerPasswordConfirm.interactable = true;
            registerEmailConfirm.interactable = true;
            registerButton.interactable = true;
            loginLink.interactable = true;
            loadingRegister.SetActive(false);
        }
        // } else if (value == 2) {
        //     loginUsername.interactable = true;
        //     loginPassword.interactable = true;
        //     loginButton.interactable = true;
        //     registerLink.interactable = true;
        //     resetButton.interactable= true;
        //     loadingLogin.SetActive(false);
        // }
    }

    public void Toggle_Changed(bool value){
        if (value){
            PlayerPrefs.SetString("remember_username", loginUsername.text);
            PlayerPrefs.SetString("remember_password", loginPassword.text);
        } else {
            PlayerPrefs.SetString("remember_username", "");
            PlayerPrefs.SetString("remember_password", "");
        }
    }

    public void ResetRegisterValues(){
        //PlayerPrefs.SetString("remember_username", "");
        PlayerPrefs.SetString("remember_password", "");
        //loginUsername.text = "";
        loginPassword.text = "";
        toggleButton.isOn = false;

    }

    public void Text_Validation_Login(string newText){
        //Debug.Log(newText);
        if (newText.Length < 6){
            errorLogin.GetComponent<Text>().text = "ALL FIELDS MUST HAVE AT LEAST 6 CHARACTERS";
            errorLogin.SetActive(true);
        }
        else if (!IsValidText(newText)){
            errorLogin.GetComponent<Text>().text = "ONLY NUMBERS OR LETTERS FOR PASSWORDS FIELDS";
            errorLogin.SetActive(true);
        } else {
            errorLogin.SetActive(false);
        }
        
    }

    public void Text_Validation_Login_Username(string newText){
        //Debug.Log(newText);
        if (newText.Length < 6){
            errorLogin.GetComponent<Text>().text = "ALL FIELDS MUST HAVE AT LEAST 6 CHARACTERS";
            errorLogin.SetActive(true);
        }
        else if (!IsValidUsername(newText)){
            errorLogin.GetComponent<Text>().text = "ONLY NUMBERS, LETTERS, DOT OR UNDERSCORE FOR USERNAMES FIELDS";
            errorLogin.SetActive(true);
        } else {
            errorLogin.SetActive(false);
        }
        
    }


    public void Text_Validation_Register(string newText){
        //Debug.Log(newText);
        if (newText.Length < 6){
            errorMessage.GetComponent<Text>().text = "ALL FIELDS MUST HAVE AT LEAST 6 CHARACTERS";
            errorMessage.SetActive(true);
        }
        else if (!IsValidText(newText)){
            errorMessage.GetComponent<Text>().text = "ONLY NUMBERS OR LETTERS FOR PASSWORDS FIELDS";
            errorMessage.SetActive(true);
        } else {
            errorMessage.SetActive(false);
        } 
    }

    public void Text_Validation_Register_Username(string newText){
        //Debug.Log(newText);
        if (newText.Length < 6){
            errorMessage.GetComponent<Text>().text = "ALL FIELDS MUST HAVE AT LEAST 6 CHARACTERS";
            errorMessage.SetActive(true);
        }
        else if (!IsValidUsername(newText)){
            errorMessage.GetComponent<Text>().text = "ONLY NUMBERS, LETTERS, DOT OR UNDERSCORE FOR USERNAMES FIELDS";
            errorMessage.SetActive(true);
        } else {
            errorMessage.SetActive(false);
        } 
    }


    public void Text_Validation_Register_Email(string newText){
        //Debug.Log(newText);
        if (newText.Length < 6){
            errorMessage.GetComponent<Text>().text = "ALL FIELDS MUST HAVE AT LEAST 6 CHARACTERS";
            errorMessage.SetActive(true);
        }
        else if (!IsValidEmail(newText)){
            errorMessage.GetComponent<Text>().text = "INVALID EMAIL FORMAT";
            errorMessage.SetActive(true);
        } else {
            errorMessage.SetActive(false);
        }
        
    }

    bool IsValidEmail(string strIn){
    // Return true if strIn is in valid e-mail format.
        return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"); 
    }

    bool IsValidText( string strIn){
        //Regex r = new Regex("^[a-zA-Z0-9]*$");
        return Regex.IsMatch(strIn, "^[a-zA-Z0-9]*$"); 
    }

    bool IsValidUsername( string strIn){
        //Regex r = new Regex("^[a-zA-Z0-9]*$");
        return Regex.IsMatch(strIn, "^[a-zA-Z0-9._]*$"); 
    }
}
