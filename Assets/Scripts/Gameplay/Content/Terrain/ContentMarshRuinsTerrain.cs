using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarshRuinsTerrain : GameTerrainBase
{
    public ContentMarshRuinsTerrain()
    {
        m_name = "MarshRuins";
        m_desc = "2 AP movement.";
        m_terrainImageNumber = 1;
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.green;

        m_isPassable = true;
        m_costToPass = 2;

        m_isWater = true;
        m_isEventTerrain = true;
    }
}
