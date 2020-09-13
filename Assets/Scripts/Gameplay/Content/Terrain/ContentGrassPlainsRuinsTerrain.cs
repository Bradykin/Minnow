﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrassPlainsRuinsTerrain : GameTerrainBase
{
    public ContentGrassPlainsRuinsTerrain()
    {
        m_name = "GrassPlainsRuins";
        m_desc = "2 AP movement.";
        m_terrainImageNumber = Random.Range(1, 3);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.yellow;

        m_isPassable = true;
        m_costToPass = 2;

        m_isEventTerrain = true;
    }
}