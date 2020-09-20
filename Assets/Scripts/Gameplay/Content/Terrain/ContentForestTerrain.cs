using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForestTerrain : GameTerrainBase
{
    public ContentForestTerrain()
    {
        m_damageReduction = 2;

        m_name = "Forest";
        m_desc = "2 AP movement.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.green;

        m_isPassable = true;
        m_costToPass = 2;

        m_isForest = true;
        m_canBurn = true;

        m_burnedTerrainType = typeof(ContentDirtForestBurnedTerrain);
    }
}
