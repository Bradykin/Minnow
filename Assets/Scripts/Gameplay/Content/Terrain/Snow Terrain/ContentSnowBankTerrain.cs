﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowBankTerrain : GameTerrainBase
{
    public ContentSnowBankTerrain()
    {
        m_rangeModifier = Constants.SnowBankRangeModifier;
        m_damageReduction = Constants.SnowBankDamageReduction;
        m_costToPass = Constants.SnowBankMovementCost;

        m_name = "SnowBank";
        m_desc = GenerateDescription();
        m_focusPanelText = GenerateFocusText();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isHill = true;
        m_isCold = true;

        LateInit();
    }
}
