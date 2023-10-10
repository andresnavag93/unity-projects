using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [ContextMenu("FadeIn")]
    public void FadeIn()
    {
        spriteRenderer.DOFade(1,2).OnComplete(()=> {
            Debug.Log("FadeIn Complete");
        });
    }

    [ContextMenu("FadeOut")]
    public void FadeOut()
    {
        spriteRenderer.DOFade(0,2).OnComplete(() => StartGame()).OnStart(()=> {
            Debug.Log("FadeOut in");
        });
    }

    private void StartGame() {
        Debug.Log("Fade Out complete");
    }

    private void Start()
    {
        FadeOut();
        GameManager.OnPlayerDeath += FadeIn;
    }

    [ContextMenu("ChangeScene")]
    public void ChangeScene( int sceneNumber)
    {
        spriteRenderer.DOFade(1,2);
        StartCoroutine(FadeInCoroutine(sceneNumber));
    }

    public IEnumerator FadeInCoroutine(int sceneNumber)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneNumber);
    }
}
