﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowForestRuinsTerrain : GameTerrainBase
{
    public ContentSnowForestRuinsTerrain()
    {
        m_damageReduction = 2;

        m_name = "SnowForestRuins";
        m_desc = "2 AP movement.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_costToPass = 2;

        m_isForest = true;
        m_isCold = true;
        m_isEventTerrain = true;

        m_completedEventType = typeof(ContentSnowForestTerrain);

        LateInit();
    }
}
