﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawnPoolData
{
    public GameEnemyUnit m_gameEnemy;

    public float m_wave;
    public float m_spawnWeight;
    public float m_priorityWeight;

    public GameSpawnPoolData(GameEnemyUnit gameEnemy, float wave, float spawnWeight, float priorityWeight)
    {
        m_gameEnemy = gameEnemy;

        m_wave = wave;
        m_spawnWeight = spawnWeight;
        m_priorityWeight = priorityWeight;
    }
}
