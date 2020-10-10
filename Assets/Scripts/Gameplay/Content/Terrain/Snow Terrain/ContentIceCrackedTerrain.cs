using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentIceCrackedTerrain : GameTerrainBase
{
    public ContentIceCrackedTerrain()
    {
        m_damageReduction = Constants.IceDamageReduction;
        m_costToPass = Constants.IceMovementCost;

        m_name = "IceCracked";
        m_desc = GenerateDescription();
        m_focusPanelText = GenerateFocusText();
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isIce = true;

        m_crackingIceTerrainType = typeof(ContentIceWaterTerrain);

        LateInit();
    }
}
