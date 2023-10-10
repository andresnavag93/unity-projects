using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private float _minSpawnInterval = 2;
    [SerializeField] private float _maxSpawnInterval = 4;

    private List<GameObject> _spawnedObstacles = new List<GameObject>();
    private bool _isSpawnAvailable;
    private float _timeToNextSpawn;

    private void Awake()
    {
        CalculateTimeToNextSpawn();
    }

    private void Update()
    {
        if (_isSpawnAvailable)
        {
            _timeToNextSpawn -= Time.deltaTime;
            if (IsTimeToSpawnObstacle())
            {
                CalculateTimeToNextSpawn();
                SpwanObstacle();
            }
        }
    }

    private void SpwanObstacle()
    {
        _spawnedObstacles.Add(Instantiate(_obstaclePrefab,
            _spawnPoints[Random.Range(0, _spawnPoints.Length - 1)].transform.position,
            Quaternion.identity));
    }

    private bool IsTimeToSpawnObstacle()
    {
        return _timeToNextSpawn < 0;
    }

    private void CalculateTimeToNextSpawn()
    {
        _timeToNextSpawn = Random.Range(_minSpawnInterval, _maxSpawnInterval);
    }

    public void DestroyProjectiles()
    {
        foreach (var projectile in _spawnedObstacles)
        {
            Destroy(projectile);
        }

        _isSpawnAvailable = false;
    }

    public void StartSpawning()
    {
        _isSpawnAvailable = true;
    }
}
