using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForestBurnedTerrain : GameTerrainBase
{
    public ContentForestBurnedTerrain()
    {
        m_damageReduction = 2;
        m_costToPass = 2;

        m_name = "ForestBurned";
        m_desc = "2 AP movement.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = Random.Range(1, 5);
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