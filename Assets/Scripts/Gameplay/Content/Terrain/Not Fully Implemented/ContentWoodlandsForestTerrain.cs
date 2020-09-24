﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWoodlandsForestTerrain : GameTerrainBase
{
    public ContentWoodlandsForestTerrain()
    {
        m_damageReduction = 1;

        m_name = "WoodlandsForest";
        m_desc = "1 AP movement.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = Random.Range(1, 5);
        m_color = Color.green;

        m_isPassable = true;
        m_costToPass = 2;

        m_isForest = true;

        LateInit();
    }
}
