﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMountainTerrain : GameTerrainBase
{
    public ContentMountainTerrain()
    {
        m_damageReduction = 4;

        m_name = "Mountain";
        m_desc = "Impassable.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.red;

        m_isPassable = false;
        m_costToPass = 2;

        m_isMountain = true;
    }
}
