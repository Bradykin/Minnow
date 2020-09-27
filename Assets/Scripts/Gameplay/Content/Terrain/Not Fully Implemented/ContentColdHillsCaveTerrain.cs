﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentColdHillsCaveTerrain : GameTerrainBase
{
    public ContentColdHillsCaveTerrain()
    {
        m_rangeModifier = 1;

        m_name = "ColdHillsCave";
        m_desc = "3 AP movement.\nRanged entities on this tile get +" + m_rangeModifier + " increased range.";
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_costToPass = 3;

        m_isHill = true;
        m_isCold = true;
        m_isCave = true;

        LateInit();
    }
}
