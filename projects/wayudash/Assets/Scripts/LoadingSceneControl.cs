using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneControl : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject loadingScreenObj;
    public Slider slider;
    AsyncOperation async;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingScene(1));
    }
    IEnumerator LoadingScene(int lvl){
        //loadingScreenObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        async = SceneManager.LoadSceneAsync(lvl);
        async.allowSceneActivation = false;

        while (async.isDone == false){
            if (async.progress == 0.1f){
                slider.value = 0.1f;
            } 
            else if (async.progress == 0.2f){
                slider.value = 0.2f;
            } else if (async.progress == 0.3f){
                slider.value = 0.3f;
            } else if (async.progress == 0.4f){
                slider.value = 0.4f;
            } else if (async.progress == 0.5f){
                slider.value = 0.5f;
            } else if (async.progress == 0.6f){
                slider.value = 0.6f;
            } else if (async.progress == 0.7f){
                slider.value = 0.7f;
            } else if (async.progress == 0.8f){
                slider.value = 0.8f;
            } else if (async.progress == 0.9f){
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
