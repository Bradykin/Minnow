using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForestBurnedRuinsTerrain : GameTerrainBase
{
    public ContentForestBurnedRuinsTerrain()
    {
        m_costToPass = 2;

        m_name = "ForestBurnedRuins";
        m_desc = "2 AP movement.";
        m_terrainImageNumber = 1;
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.green;

        m_isPassable = true;
        m_isForest = true;
        m_isBurned = true;
        m_isEventTerrain = true;

        m_unburnedTerrainType = typeof(ContentForestRuinsTerrain);
        m_completedEventType = typeof(ContentForestBurnedTerrain);
    }
}

