using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTundraHillsCaveTerrain : GameTerrainBase
{
    public ContentTundraHillsCaveTerrain()
    {
        m_rangeModifier = Constants.HillsRangeModifier;
        m_damageReduction = Constants.HillsDamageReduction;
        m_costToPass = Constants.HillsMovementCost;

        m_name = "TundraHillsCave";
        m_desc = GenerateDescription();
        m_focusPanelText = GenerateFocusText();
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isHill = true;
        m_isCold = true;
        m_isCave = true;

        LateInit();
    }
}
