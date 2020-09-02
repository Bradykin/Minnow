using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWaterTerrain : GameTerrainBase
{
    public ContentWaterTerrain()
    {
        m_name = "Water";
        m_desc = "Impassable.";
        m_terrainNumber = Random.Range(1, 5);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainNumber);
        m_color = Color.blue;

        m_isPassable = false;
        m_costToPass = 1;
    }
}

