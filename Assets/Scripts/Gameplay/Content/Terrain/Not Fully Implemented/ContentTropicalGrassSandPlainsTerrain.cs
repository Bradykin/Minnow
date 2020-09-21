using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTropicalGrassSandPlainsTerrain : GameTerrainBase
{
    public ContentTropicalGrassSandPlainsTerrain()
    {
        m_name = "TropicalGrassSandPlains";
        m_desc = "Simple, no changes.";
        m_terrainImageNumber = Random.Range(1, 5);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.white;

        m_isPassable = true;
        m_costToPass = 1;
    }
}
