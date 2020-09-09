using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowHillsPondTerrain : GameTerrainBase
{
    public ContentDesertYellowHillsPondTerrain()
    {
        m_name = "DesertYellowHillsPond";
        m_desc = "2 AP movement.";
        m_terrainImageNumber = Random.Range(1, 5);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.grey;

        m_isPassable = true;
        m_costToPass = 2;

        m_isHill = true;
        m_isHot = true;
        m_isWater = true;
    }
}
