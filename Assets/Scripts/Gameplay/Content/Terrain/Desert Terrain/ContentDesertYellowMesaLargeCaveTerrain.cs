using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowMesaLargeCaveTerrain : GameTerrainBase
{
    public ContentDesertYellowMesaLargeCaveTerrain()
    {
        m_damageReduction = Constants.MountainsDamageReduction;
        m_costToPass = Constants.MountainsMovementCost;
        m_isPassable = false;

        m_name = "DesertYellowMesaLargeCave";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isMountain = true;
        m_isHot = true;
        m_isCave = true;

        LateInit();
    }
}
