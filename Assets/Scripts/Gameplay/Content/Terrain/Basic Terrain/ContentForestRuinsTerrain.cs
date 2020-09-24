using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForestRuinsTerrain : GameTerrainBase
{
    public ContentForestRuinsTerrain()
    {
        m_damageReduction = Mathf.Max(Constants.ForestDamageReduction, Constants.RuinsDamageReduction);
        m_costToPass = Mathf.Max(Constants.ForestMovementCost, Constants.RuinsMovementCost);

        m_name = "ForestRuins";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;
        m_color = Color.green;

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentForestBurnedRuinsTerrain);
        m_completedEventType = typeof(ContentForestTerrain);

        LateInit();
    }
}
