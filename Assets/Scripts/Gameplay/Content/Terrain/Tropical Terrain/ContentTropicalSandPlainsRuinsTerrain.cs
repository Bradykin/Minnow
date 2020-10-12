﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTropicalSandPlainsRuinsTerrain : GameTerrainBase
{
    public ContentTropicalSandPlainsRuinsTerrain()
    {
        m_damageReduction = Mathf.Max(Constants.PlainsDamageReduction, Constants.RuinsDamageReduction);
        m_costToPass = Mathf.Max(Constants.PlainsMovementCost, Constants.RuinsMovementCost);

        m_name = "TropicalSandPlainsRuins";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_isEventTerrain = true;

        m_completedEventTerrainType = typeof(ContentTropicalSandPlainsTerrain);

        LateInit();
    }
}