using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTropicalSandForestRuinsTerrain : GameTerrainBase
{
    public ContentTropicalSandForestRuinsTerrain()
    {
        m_damageReduction = Mathf.Max(Constants.ForestDamageReduction, Constants.RuinsDamageReduction);
        m_costToPass = Mathf.Max(Constants.ForestMovementCost, Constants.RuinsMovementCost);

        m_name = "TropicalSandForestRuins";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentTropicalSandPlainsRuinsTerrain);
        m_completedEventTerrainType = typeof(ContentTropicalSandForestTerrain);

        LateInit();
    }
}
