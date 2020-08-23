using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyFactory
{
    public static GameEntity GetRandomEnemy()
    {
        int r = Random.Range(0, 3);

        switch (r)
        {
            case 0:
                return new ContentSlimeEnemy();
            case 1:
                return new ContentSeigebreakerEntity();
            case 2:
                return new ContentShadeEnemy();
            default:
                return null;
        }
    }
}