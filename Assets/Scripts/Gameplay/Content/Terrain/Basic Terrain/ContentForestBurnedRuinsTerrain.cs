using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForestBurnedRuinsTerrain : GameTerrainBase
{
    public ContentForestBurnedRuinsTerrain()
    {
        m_damageReduction = Mathf.Max(Constants.BurnedForestDamageReduction, Constants.RuinsDamageReduction);
        m_costToPass = Mathf.Max(Constants.BurnedForestMovementCost, Constants.RuinsMovementCost);

        m_name = "ForestBurnedRuins";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_isForest = true;
        m_isHot = true;
        m_isBurned = true;
        m_isEventTerrain = true;

        m_unburnedTerrainType = typeof(ContentForestRuinsTerrain);
        m_completedEventType = typeof(ContentForestBurnedTerrain);

        LateInit();
    }
}

