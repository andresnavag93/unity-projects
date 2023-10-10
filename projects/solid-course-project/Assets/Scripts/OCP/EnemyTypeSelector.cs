using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class EnemyTypeSelector
{
    private List<string> enemyIds = new List<string>();

    public EnemyTypeSelector(List<string> enemyIds)
    {
        this.enemyIds = enemyIds;
    }

    public string Select()
    {
        var randomTypeIndex = Random.Range(0, enemyIds.Count);
        return enemyIds[randomTypeIndex];
    }
}
