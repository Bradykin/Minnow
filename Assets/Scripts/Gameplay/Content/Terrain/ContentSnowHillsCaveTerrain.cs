using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowHillsCaveTerrain : GameTerrainBase
{
    public ContentSnowHillsCaveTerrain()
    {
        m_rangeModifier = 1;

        m_name = "SnowHillsCave";
        m_desc = "3 AP movement.\nRanged entities on this tile get +" + m_rangeModifier + " increased range.";
        m_terrainImageNumber = 1;
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.grey;

        m_isPassable = true;
        m_costToPass = 3;

        m_isHill = true;
        m_isCold = true;
        m_isCave = true;
    }
}
