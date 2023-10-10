using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Text _lastGameDurationText;

    private ILoader _load;
    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonPressed);
    }

    public void Configure(ILoader load)
    {
        _load = load;
    }

    private void OnEnable()
    {
        LoadLastDuration();
    }

    private void LoadLastDuration()
    {
        var gameDuration = _load.LoadData();
        var time = TimeSpan.FromSeconds(gameDuration);
        _lastGameDurationText.text = FormatTimer(time);
    }

    private static string FormatTimer(TimeSpan time)
    {
        return time.ToString(@"mm\:ss");
    }

    private void OnStartButtonPressed()
    {
        GameEventListener.Instance.OnStartGame();
        gameObject.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameObject.SetActive(true);
    }
}
