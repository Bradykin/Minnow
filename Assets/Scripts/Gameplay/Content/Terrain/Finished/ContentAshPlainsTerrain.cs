﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAshPlainsTerrain : GameTerrainBase
{
    public ContentAshPlainsTerrain()
    {
        m_name = "AshPlains";
        m_desc = "Simple, no changes.";
        m_terrainImageNumber = Random.Range(1, 5);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.white;

        m_isPassable = true;
        m_costToPass = 1;

        m_isHot = true;
    }
}
