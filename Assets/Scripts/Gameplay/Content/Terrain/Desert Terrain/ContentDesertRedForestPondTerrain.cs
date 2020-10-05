using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedForestPondTerrain : GameTerrainBase
{
    public ContentDesertRedForestPondTerrain()
    {
        m_damageReduction = Constants.ForestDamageReduction;
        m_costToPass = Constants.ForestMovementCost;

        m_name = "DesertRedForestPond";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;
        m_isHot = true;
        m_isWaterSource = true;

        m_burnedTerrainType = typeof(ContentDesertRedDirtPlainsRuinsTerrain);

        LateInit();
    }
}