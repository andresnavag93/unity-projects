using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;

public class WeatherChange : MonoBehaviour
{
    private int actualWeather;
    [SerializeField] DigitalRuby.RainMaker.RainScript2D rainMaker;

    void Start()
    {
        StartCoroutine(GetWeather());
    }

    private void WeatherChanger()
    {
        if (actualWeather >= 200 && actualWeather < 300)
        {
            //tormenta
            rainMaker.RainIntensity += 1;
        }
        else if (actualWeather >= 300 && actualWeather < 400)
        {
            //tormenta
            rainMaker.RainIntensity += 0.2f;
        }
        else if (actualWeather >= 400 && actualWeather < 500)
        {
            //tormenta
            rainMaker.RainIntensity += 0.55f;
        }
        else if (actualWeather >= 500 && actualWeather < 600)
        {
            //tormenta
            rainMaker.RainIntensity += 0.7f;
        }
        else if (actualWeather >= 700 && actualWeather < 800)
        {
            //niebla
            rainMaker.RainIntensity += 0.1f;
        }
        else if (actualWeather > 800)
        {
            //niebla
            rainMaker.RainIntensity += 0.1f;
        }
        else if (actualWeather == 800)
        {
            //ClearSky
            rainMaker.gameObject.SetActive(false);
        }
    }

    IEnumerator GetWeather()
    {
        UnityWebRequest www = UnityWebRequest.Get("api.openweathermap.org/data/2.5/weather?q=caracas&appid=2d5ed33c64cfef4e50bf14f8872522ab");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError((www.error));
            actualWeather = 800;
        }
        else
        {
            JsonData jsonData = JsonMapper.ToObject(www.downloadHandler.text);
            actualWeather = (int)jsonData["weather"][0]["id"];
        }
        Debug.Log(actualWeather);
        WeatherChanger();
        StopCoroutine(GetWeather());
    }
}
