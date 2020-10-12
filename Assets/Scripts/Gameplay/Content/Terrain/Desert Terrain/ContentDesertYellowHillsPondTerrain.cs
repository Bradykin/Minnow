﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowHillsPondTerrain : GameTerrainBase
{
    public ContentDesertYellowHillsPondTerrain()
    {
        m_rangeModifier = Constants.HillsRangeModifier;
        m_damageReduction = Constants.HillsDamageReduction;
        m_costToPass = Constants.HillsMovementCost;

        m_name = "DesertYellowHillsPond";
        m_desc = GenerateDescription();
        m_focusPanelText = GenerateFocusText();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isHot = true;
        m_isHill = true;
        m_isWaterSource = true;

        LateInit();
    }
}