﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowPlainsTerrain : GameTerrainBase
{
    public ContentSnowPlainsTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "Snow Plains";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_isSnow = true;

        LateInit();
    }
}
