using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForestRuinsTerrain : GameTerrainBase
{
    public ContentForestRuinsTerrain()
    {
        m_damageReduction = 2;
        m_costToPass = 2;

        m_name = "ForestRuins";
        m_desc = "2 AP movement.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.green;

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentForestBurnedRuinsTerrain);
        m_completedEventType = typeof(ContentForestTerrain);
    }
}
