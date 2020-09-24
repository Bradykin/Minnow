﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTropicalPlainsTerrain : GameTerrainBase
{
    public ContentTropicalPlainsTerrain()
    {
        m_name = "TropicalPlains";
        m_desc = "Simple, no changes.";
        m_terrainImageNumber = Random.Range(1, 5);
        m_color = Color.white;

        m_isPassable = true;
        m_costToPass = 1;

        LateInit();
    }
}
