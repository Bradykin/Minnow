using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAshForestBurnedTerrain : GameTerrainBase
{
    public ContentAshForestBurnedTerrain()
    {
        m_damageReduction = Constants.BurnedForestDamageReduction;
        m_costToPass = Constants.BurnedForestMovementCost;

        m_name = "AshForestBurned";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isForest = true;
        m_isHot = true;
        m_isBurned = true;

        LateInit();
    }
}