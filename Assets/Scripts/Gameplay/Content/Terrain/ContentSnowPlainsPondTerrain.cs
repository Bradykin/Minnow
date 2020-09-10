using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowPlainsPondTerrain : GameTerrainBase
{
    public ContentSnowPlainsPondTerrain()
    {
        m_name = "SnowPlainsPond";
        m_desc = "Simple, no changes.";
        m_terrainImageNumber = 1;
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.white;

        m_isPassable = true;
        m_costToPass = 1;

        m_isCold = true;
        m_isWater = true;
    }
}
