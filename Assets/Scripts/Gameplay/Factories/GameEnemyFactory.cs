using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyFactory
{
    public static GameEnemyEntity GetRandomEnemy()
    {
        int r = Random.Range(0, 4);

        switch (r)
        {
            case 0:
                return new ContentSlimeEnemy();
            case 1:
                return new ContentSeigebreakerEntity();
            case 2:
                return new ContentShadeEnemy();
            case 3:
                return new ContentSpinnerEnemy();
            default:
                return null;
        }
    }
}