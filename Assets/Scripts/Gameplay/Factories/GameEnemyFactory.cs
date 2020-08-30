using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyFactory
{
    public static GameEnemyEntity GetRandomEnemy(GameOpponent gameOpponent)
    {
        int r = Random.Range(0, 4);

        switch (r)
        {
            case 0:
                return new ContentSlimeEnemy(gameOpponent);
            case 1:
                return new ContentSeigebreakerEntity(gameOpponent);
            case 2:
                return new ContentShadeEnemy(gameOpponent);
            case 3:
                return new ContentSpinnerEnemy(gameOpponent);
            default:
                return null;
        }
    }
}