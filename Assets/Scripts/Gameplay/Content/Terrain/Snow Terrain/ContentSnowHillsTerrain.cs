using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowHillsTerrain : GameTerrainBase
{
    public ContentSnowHillsTerrain()
    {
        m_rangeModifier = Constants.HillsRangeModifier;
        m_damageReduction = Constants.HillsDamageReduction;
        m_costToPass = Constants.HillsMovementCost;

        m_name = "SnowHills";
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
