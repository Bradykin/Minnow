using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentColdHillsCaveTerrain : GameTerrainBase
{
    public ContentColdHillsCaveTerrain()
    {
        m_rangeModifier = Constants.HillsRangeModifier;
        m_damageReduction = Constants.HillsDamageReduction;
        m_costToPass = Constants.HillsMovementCost;

        m_name = "ColdHillsCave";
        m_desc = GenerateDescription();
        m_focusPanelText = GenerateFocusText();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isHill = true;
        m_isCold = true;
        m_isCave = true;

        LateInit();
    }
}
