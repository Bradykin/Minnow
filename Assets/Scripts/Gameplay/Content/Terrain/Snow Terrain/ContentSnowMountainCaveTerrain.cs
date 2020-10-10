﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowMountainCaveTerrain : GameTerrainBase
{
    public ContentSnowMountainCaveTerrain()
    {
        m_damageReduction = Constants.MountainsDamageReduction;
        m_costToPass = Constants.MountainsMovementCost;
        m_isPassable = false;

        m_name = "SnowMountainCave";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isMountain = true;
        m_isCold = true;
        m_isCave = true;

        LateInit();
    }
}
