using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyFactory
{
    public static GameEntity GetRandomEnemy()
    {
        int r = Random.Range(0, 2);

        switch (r)
        {
            case 0:
                return new GameSlimeEnemy();
            case 1:
                return new GameSeigebreakerEntity();
            default:
                return null;
        }
    }
}