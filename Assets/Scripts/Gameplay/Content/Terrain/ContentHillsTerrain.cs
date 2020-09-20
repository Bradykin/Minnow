using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHillsTerrain : GameTerrainBase
{
    public ContentHillsTerrain()
    {
        m_rangeModifier = 1;
        m_damageReduction = 2;

        m_name = "Hills";
        m_desc = "3 AP movement.\nRanged entities on this tile get +" + m_rangeModifier + " increased range.";
        m_focusPanelText = "Ranged entities on this tile get +" + m_rangeModifier + " increased range.";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.grey;

        m_isPassable = true;
        m_costToPass = 3;

        m_isHill = true;
    }
}
