﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoInactiveTerrain : GameTerrainBase
{
    public ContentVolcanoInactiveTerrain()
    {
        m_damageReduction = 4;

        m_name = "VolcanoInactive";
        m_desc = "Impassable.\nUnits on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = false;
        m_costToPass = 2;

        m_isMountain = true;
        m_isHot = true;
        m_isVolcano = true;

        LateInit();
    }
}
