using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyFactory
{
    private static List<GameEnemyEntity> m_enemies = new List<GameEnemyEntity>();
    private static List<GameEnemyEntity> m_commonEnemies = new List<GameEnemyEntity>();

    private static bool hasInit = false;
    
    public static void Init()
    {
        m_enemies.Add(new ContentSlimeEnemy(null));
        m_enemies.Add(new ContentSeigebreakerEntity(null));
        m_enemies.Add(new ContentShadeEnemy(null));
        m_enemies.Add(new ContentSpinnerEnemy(null));
        hasInit = true;
    }
    
    public static GameEnemyEntity GetRandomEnemy(GameOpponent gameOpponent)
    {
        if (!hasInit)
            Init();
        
        int r = UnityEngine.Random.Range(0, m_enemies.Count);

        return (GameEnemyEntity)Activator.CreateInstance(m_enemies[r].GetType(), gameOpponent);
    }
}