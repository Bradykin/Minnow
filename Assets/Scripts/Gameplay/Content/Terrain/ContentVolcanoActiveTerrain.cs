using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoActiveTerrain : GameTerrainBase
{
    public ContentVolcanoActiveTerrain()
    {
        m_damageReduction = 4;

        m_name = "VolcanoActive";
        m_desc = "Impassable.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = Random.Range(1, 5);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.red;

        m_isPassable = false;
        m_costToPass = 2;

        m_isMountain = true;
        m_isHot = true;
        m_isVolcano = true;
    }
}
