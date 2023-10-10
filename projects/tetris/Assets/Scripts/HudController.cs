using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI maxScoreText;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
        scoreText.text = "Score: 0";
        maxScoreText.text = "Max Score: " + PlayerPrefs.GetInt("maxScore", 0);
        levelText.text = "Level: 1";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLevel(int level)
    {
        levelText.text = "Level: " + level.ToString();
    }

    public void UpdateMaxScore(int score)
    {
        maxScoreText.text = "Max Score: " + score.ToString();
    }

    public void ActiveGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

}
