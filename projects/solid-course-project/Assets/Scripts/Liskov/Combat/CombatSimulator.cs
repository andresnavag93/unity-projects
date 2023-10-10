using System.Collections.Generic;
using UnityEngine;


public class CombatSimulator
{
    public void Simulate(List<AbstractHero> team1, List<AbstractHero> team2)
    {
        foreach (var hero in team1)
        {
            var opponent = team2[Random.Range(0, team2.Count - 1)];
            hero.DoAttack(opponent);
        }
    }
}

