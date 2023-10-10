using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UserModel
{
  public string email, email_confirm, instagram_user, instagram_user_confirm,
      password, password_confirm, token, first_place, second_place, third_place, fourth_place, fifth_place,
       first_place_max_coins, second_place_max_coins, third_place_max_coins, fourth_place_max_coins, fifth_place_max_coins,
       first_place_max_score, second_place_max_score, third_place_max_score, fourth_place_max_score, fifth_place_max_score;
  public int tries, max_coins, id, ranking;
  public float max_score;
}

public class ErrorModel
{
  public List<string> error;
}

public class HttpRequestManager : MonoBehaviour
{
  public static HttpRequestManager sharedInstance;
  GameObject error, loadingGame;
  string uri = "https://wayu-dash-backend.herokuapp.com/";
  //string uri = "http://127.0.0.1:3000/";
  string route, token;

  Button playButton, statsButton, instructionButton;
  int id;
  void Awake()
  {
    if (sharedInstance == null)
    {
      sharedInstance = this;
    }
  }

  public void PutUpdateNewUserScore(UserModel user)
  {
    token = PlayerPrefs.GetString("auth_token", "");
    id = PlayerPrefs.GetInt("auth_id", 0);
    if (token.Equals("") || id == 0)
    {
      PlayerPrefs.SetString("auth_token", "");
      PlayerPrefs.SetInt("auth_id", 0);
      //loadingGame.SetActive(false);
      error = InputFieldManager.sharedInstance.errorLogin;
      error.GetComponent<Text>().text = "SESSION EXPIRED";
      error.SetActive(true);
      MenuManager.sharedInstance.ShowLoginMode();
    }
    else
    {
      route = uri + "users/" + id.ToString();
      StartCoroutine(PutUpdateScore(route, user, token));
    }
  }

  public IEnumerator PutUpdateScore(string uri, UserModel user, string token)
  {
    string json = JsonUtility.ToJson(user);
    byte[] form = System.Text.Encoding.UTF8.GetBytes(json);
    var www = new UnityWebRequest(uri, "PUT");
    byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
    www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
    www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    www.SetRequestHeader("Content-Type", "application/json");
    www.SetRequestHeader("authorization", "JWT " + token);

    error = InputFieldManager.sharedInstance.errorLogin;
    loadingGame = InputFieldManager.sharedInstance.loadingScore;
    playButton = InputFieldManager.sharedInstance.playButton;
    statsButton = InputFieldManager.sharedInstance.statsButton;
    instructionButton = InputFieldManager.sharedInstance.instructionButton;

    error.SetActive(false);
    loadingGame.SetActive(true);
    playButton.interactable = false;
    statsButton.interactable = false;
    instructionButton.interactable = false;
    yield return www.SendWebRequest();
    if (!ErrorInRequest(www))
    {
      UserModel obj = new UserModel();
      obj = JsonUtility.FromJson<UserModel>(www.downloadHandler.text);
      InputFieldManager.sharedInstance.UpdateScores(obj);
    }
    loadingGame.SetActive(false);
    playButton.interactable = true;
    statsButton.interactable = true;
    instructionButton.interactable = true;
  }

  public void PostRecoveryPasswordUser(UserModel user)
  {
    route = "users/new_password/";
    StartCoroutine(GenericPostUser(uri + route, user, 2));
  }

  public void PostCreateNewUser(UserModel user)
  {
    route = "users/";
    StartCoroutine(GenericPostUser(uri + route, user, 1));
  }

  public void PostLoginWithUsername(UserModel user)
  {
    route = "auth_user/";
    StartCoroutine(GenericPostUser(uri + route, user, 0));

  }

  public void PostUserLogout()
  {
    PlayerPrefs.SetString("auth_token", "");
    PlayerPrefs.SetInt("auth_id", 0);
    MenuManager.sharedInstance.ShowLoginMode();
  }

  public bool ErrorInRequest(UnityWebRequest www)
  {
    if (www.isNetworkError)
    {
      error.GetComponent<Text>().text = "NETWORK ERROR";
      error.SetActive(true);
      PlayerPrefs.SetString("auth_token", "");
      PlayerPrefs.SetInt("auth_id", 0);
      MenuManager.sharedInstance.ShowLoginMode();
      return true;
    }
    else if (www.isHttpError)
    {
      if (www.responseCode == 406)
      {
        error.GetComponent<Text>().text = "SESSION EXPIRED";
        error.SetActive(true);
        PlayerPrefs.SetString("auth_token", "");
        PlayerPrefs.SetInt("auth_id", 0);
        MenuManager.sharedInstance.ShowLoginMode();
      }
      else if (www.responseCode == 422)
      {
        ErrorModel customError = new ErrorModel();
        customError = JsonUtility.FromJson<ErrorModel>(www.downloadHandler.text);
        string text = "";
        for (int i = 0; i < customError.error.Count; i++)
        {
          if (i == (customError.error.Count - 1))
          {
            text += customError.error[i];
          }
          else
          {
            text += customError.error[i] + ", ";
          }
        }
        error.GetComponent<Text>().text = text;
        error.SetActive(true);
      }
      else if (www.responseCode == 401)
      {
        error.GetComponent<Text>().text = "INSUFFICIENT PERMITS";
        error.SetActive(true);
      }
      return true;
    }
    return false;
  }

  public IEnumerator GenericPostUser(string uri, UserModel user, int value)
  {
    string json = JsonUtility.ToJson(user);
    byte[] form = System.Text.Encoding.UTF8.GetBytes(json);
    var www = new UnityWebRequest(uri, "POST");
    byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
    www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
    www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    www.SetRequestHeader("Content-Type", "application/json");
    error = InputFieldManager.sharedInstance.ActiveInputFieldButton(value);
    error.SetActive(false);

    yield return www.SendWebRequest();
    if (!ErrorInRequest(www))
    {
      if (value != 2)
      {
        UserModel obj = new UserModel();
        obj = JsonUtility.FromJson<UserModel>(www.downloadHandler.text);
        PlayerPrefs.SetString("auth_token", obj.token);
        PlayerPrefs.SetInt("auth_id", obj.id);
        token = PlayerPrefs.GetString("auth_token", "");
        int id = PlayerPrefs.GetInt("auth_id", 0);
        InputFieldManager.sharedInstance.UpdateScores(obj);
        MenuManager.sharedInstance.ShowMenuPlayMode();
      }
      else
      {
        InputFieldManager.sharedInstance.ResetRegisterValues();
        InputFieldManager.sharedInstance.successEmail.SetActive(true);
        yield return new WaitForSeconds(4f);
        InputFieldManager.sharedInstance.successEmail.SetActive(false);
      }
    }
    InputFieldManager.sharedInstance.InActiveInputFieldButton(value);

  }

}
