﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRuinsTerrain : GameTerrainBase
{
    public ContentDesertRuinsTerrain()
    {
        m_damageReduction = 2;

        m_name = "DesertRuins";
        m_desc = "2 AP movement.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = Random.Range(1, 3);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.yellow;

        m_isPassable = true;
        m_costToPass = 2;

        m_isForest = true;
        m_isEventTerrain = true;

        m_completedEventType = typeof(ContentDesertDunesTerrain);
    }
}
