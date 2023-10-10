using UnityEngine;

/* Manage the state of the game */
public class GameEventListener : MonoBehaviour
{
    public static GameEventListener Instance { get; private set; }

    [SerializeField] private MainMenu _menu;
    [SerializeField] private ObstacleSpawner _obstacleSpawner;
    [SerializeField] private ItemsSpawner _itemSpawner;
    [SerializeField] private Player _player;

    private float _gameStartTime;

    private ISaver _save;

    private void Awake()
    {
        Instance = this;
    }

    public void Configure(ISaver save)
    {
        _save = save;
    }

    public void OnPlayerDeath()
    {
        CleanPendingElements();
        SaveLastDuration();
        ShowGameOver();
    }
    private void CleanPendingElements()
    {
        _obstacleSpawner.DestroyProjectiles();
        _itemSpawner.DestroyItems();
    }

    private void SaveLastDuration()
    {
        var gameDuration = Time.time - _gameStartTime;
        _save.SaveData(gameDuration);
    }

    private void ShowGameOver()
    {
        _menu.ShowGameOver();
    }

    public void OnStartGame()
    {
        StartTimer();
        Initialize();
    }

    private void Initialize()
    {
        _obstacleSpawner.StartSpawning();
        _itemSpawner.StartSpawning();
        _player.Reset();
    }

    private void StartTimer()
    {
        _gameStartTime = Time.time;
    }
}
