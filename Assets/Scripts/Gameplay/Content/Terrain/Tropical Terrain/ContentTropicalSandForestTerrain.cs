using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTropicalSandForestTerrain : GameTerrainBase
{
    public ContentTropicalSandForestTerrain()
    {
        m_damageReduction = Constants.ForestDamageReduction;
        m_costToPass = Constants.ForestMovementCost;

        m_name = "TropicalSandForest";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;

        m_burnedTerrainType = typeof(ContentTropicalSandPlainsTerrain);
        m_addedEventTerrainType = typeof(ContentTropicalSandForestRuinsTerrain);

        LateInit();
    }
}
