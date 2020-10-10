using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedMesaLargeCaveTerrain : GameTerrainBase
{
    public ContentDesertRedMesaLargeCaveTerrain()
    {
        m_damageReduction = Constants.MountainsDamageReduction;
        m_costToPass = Constants.MountainsMovementCost;
        m_isPassable = false;

        m_name = "DesertRedMesaLargeCave";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isMountain = true;
        m_isHot = true;
        m_isCave = true;

        LateInit();
    }
}
