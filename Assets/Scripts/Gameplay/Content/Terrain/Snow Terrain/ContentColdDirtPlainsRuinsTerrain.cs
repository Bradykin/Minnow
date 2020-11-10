﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentColdDirtPlainsRuinsTerrain : GameTerrainBase
{
    public ContentColdDirtPlainsRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "ColdDirtPlainsRuins";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_isCold = true;
        m_isEventTerrain = true;

        m_completedEventTerrainType = typeof(ContentColdDirtPlainsTerrain);

        LateInit();
    }
}
