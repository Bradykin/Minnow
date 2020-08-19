using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyFactory
{
    public static GameEntity GetRandomEnemy()
    {
        int r = Random.Range(0, 1);

        switch (r)
        {
            case 0:
                return new GameEntitySlimeEnemy();
            default:
                return null;
        }
    }
}