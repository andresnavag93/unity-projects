using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;

    private Dictionary<string, Enemy> idToEnemies = new Dictionary<string, Enemy>();

    private void Awake()
    {
        foreach (var enemy in enemies)
        {
            idToEnemies.Add(enemy.Id, enemy);
        }
    }

    public Enemy Create(string enemyId)
    {
        Enemy enemyToSpawn;
        if (!idToEnemies.TryGetValue(enemyId, out enemyToSpawn))
        {
            throw new ArgumentException($"No enemy with id {enemyId}");
        }

        return Instantiate(enemyToSpawn);
    }
}
