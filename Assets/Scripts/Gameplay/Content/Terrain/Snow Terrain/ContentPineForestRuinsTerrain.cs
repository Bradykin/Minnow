using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPineForestRuinsTerrain : GameTerrainBase
{
    public ContentPineForestRuinsTerrain()
    {
        m_damageReduction = Mathf.Max(Constants.ForestDamageReduction, Constants.RuinsDamageReduction);
        m_costToPass = Mathf.Max(Constants.ForestMovementCost, Constants.RuinsMovementCost);

        m_name = "PineForestRuins";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_isForest = true;
        m_isCold = true;
        m_canBurn = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentForestBurnedRuinsTerrain);
        m_completedEventTerrainType = typeof(ContentPineForestTerrain);

        LateInit();
    }
}
