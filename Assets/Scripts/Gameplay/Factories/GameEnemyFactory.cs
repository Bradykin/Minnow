using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyFactory
{
    public static GameEnemyEntity GetRandomEnemy(GameOpponent gameOpponent)
    {
        int r = UnityEngine.Random.Range(0, 4);

        switch (r)
        {
            case 0:
                return GetEnemyEntityInstance<ContentSlimeEnemy>(gameOpponent);
            case 1:
                return GetEnemyEntityInstance<ContentSeigebreakerEntity>(gameOpponent);
            case 2:
                return GetEnemyEntityInstance<ContentShadeEnemy>(gameOpponent);
            case 3:
                return GetEnemyEntityInstance<ContentSpinnerEnemy>(gameOpponent);
            default:
                return null;
        }
    }

    private static T GetEnemyEntityInstance<T>(GameOpponent gameOpponent) where T : GameEnemyEntity
    {
        return (T)Activator.CreateInstance(typeof(T), gameOpponent);
    }
}