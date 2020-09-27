﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowMesaLargeTerrain : GameTerrainBase
{
    public ContentDesertYellowMesaLargeTerrain()
    {
        m_damageReduction = 4;

        m_name = "DesertYellowMesaLarge";
        m_desc = "Impassable.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = Random.Range(1, 3);

        m_isPassable = false;
        m_costToPass = 2;

        m_isMountain = true;
        m_isHot = true;

        LateInit();
    }
}
