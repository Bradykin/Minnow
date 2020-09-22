﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowCraterTerrain : GameTerrainBase
{
    public ContentDesertYellowCraterTerrain()
    {
        m_name = "DesertYellowCrater";
        m_desc = "2 AP movement.";
        m_terrainImageNumber = 1;
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.green;

        m_isPassable = true;
        m_costToPass = 2;

        m_isForest = true;
        m_isHot = true;
    }
}