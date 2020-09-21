using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWaterTerrain : GameTerrainBase
{
    public ContentWaterTerrain()
    {
        m_costToPass = 1;

        m_name = "Water";
        m_desc = "Impassable.";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.blue;

        m_isPassable = false;
        m_isWater = true;
    }
}

