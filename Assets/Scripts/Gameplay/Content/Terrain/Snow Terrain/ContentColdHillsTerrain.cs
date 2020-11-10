﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentColdHillsTerrain : GameTerrainBase
{
    public ContentColdHillsTerrain()
    {
        m_rangeModifier = Constants.HillsRangeModifier;
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "ColdHills";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isHill = true;
        m_isCold = true;

        LateInit();
    }
}
