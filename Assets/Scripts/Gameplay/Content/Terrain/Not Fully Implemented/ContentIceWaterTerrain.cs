using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentIceWaterTerrain : GameTerrainBase
{
    public ContentIceWaterTerrain()
    {
        m_name = "IceWater";
        m_desc = "Impassable.";
        m_terrainImageNumber = Random.Range(1, 5);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.blue;

        m_isPassable = false;
        m_costToPass = 1;

        m_isWater = true;
        m_isCold = true;
    }
}

