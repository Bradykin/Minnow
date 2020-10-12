﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowForestTerrain : GameTerrainBase
{
    public ContentSnowForestTerrain()
    {
        m_damageReduction = Constants.ForestDamageReduction;
        m_costToPass = Constants.ForestMovementCost;

        m_name = "SnowForest";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isForest = true;
        m_isCold = true;
        m_canBurn = true;

        m_burnedTerrainType = typeof(ContentForestBurnedTerrain);
        m_addedEventTerrainType = typeof(ContentSnowForestRuinsTerrain);

        LateInit();
    }
}