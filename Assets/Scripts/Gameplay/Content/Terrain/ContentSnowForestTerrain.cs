using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowForestTerrain : GameTerrainBase
{
    public ContentSnowForestTerrain()
    {
        m_damageReduction = 2;

        m_name = "SnowForest";
        m_desc = "2 AP movement.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = Random.Range(1, 5);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.green;

        m_isPassable = true;
        m_costToPass = 2;

        m_isForest = true;
        m_isCold = true;
    }
}
