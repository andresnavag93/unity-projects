using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<string> enemyIds;
    [SerializeField] private EnemyFactory enemyFactory;

    private EnemyTypeSelector enemyTypeSelector;

    private void Start()
    {
        enemyTypeSelector = new EnemyTypeSelector(enemyIds);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        var enemyTypeToSpawn = enemyTypeSelector.Select();
        enemyFactory.Create(enemyTypeToSpawn);
    }
}
