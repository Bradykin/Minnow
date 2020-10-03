﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPineForestRuinsTerrain : GameTerrainBase
{
    public ContentPineForestRuinsTerrain()
    {
        m_damageReduction = 2;

        m_name = "PineForestRuins";
        m_desc = "2 Stamina movement.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_costToPass = 2;

        m_isForest = true;
        m_isEventTerrain = true;

        m_completedEventType = typeof(ContentPineForestTerrain);

        LateInit();
    }
}
