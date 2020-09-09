using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowMesaCaveTerrain : GameTerrainBase
{
    public ContentDesertYellowMesaCaveTerrain()
    {
        m_rangeModifier = 2;

        m_name = "DesertYellowMesaCave";
        m_desc = "4 AP movement.\nRanged entities on this tile get +" + m_rangeModifier + " increased range.";
        m_terrainImageNumber = Random.Range(1, 5);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.grey;

        m_isPassable = true;
        m_costToPass = 4;

        m_isHill = true;
        m_isHot = true;
        m_isCave = true;
    }
}
