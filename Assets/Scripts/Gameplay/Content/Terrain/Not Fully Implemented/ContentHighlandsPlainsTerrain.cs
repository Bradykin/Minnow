﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHighlandsPlainsTerrain : GameTerrainBase
{
    public ContentHighlandsPlainsTerrain()
    {
        m_name = "HighlandsPlains";
        m_desc = "Simple, no changes.";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;
        m_costToPass = 1;

        LateInit();
    }
}
