using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForestBurnedTerrain : GameTerrainBase
{
    public ContentForestBurnedTerrain()
    {
        m_damageReduction = Constants.BurnedForestDamageReduction;
        m_costToPass = Constants.BurnedForestMovementCost;

        m_name = "ForestBurned";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.grey;

        m_isPassable = true;
        m_isForest = true;
        m_isHot = true;
        m_isBurned = true;

        m_unburnedTerrainType = typeof(ContentForestTerrain);
        m_addedEventType = typeof(ContentForestBurnedRuinsTerrain);
    }
}