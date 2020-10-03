using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowSaltFlatsTerrain : GameTerrainBase
{
    public ContentDesertYellowSaltFlatsTerrain()
    {
        m_damageReduction = Constants.RuinsDamageReduction;
        m_costToPass = Constants.RuinsMovementCost;

        m_name = "DesertYellowSaltFlats";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_canBurn = false;
        m_isHot = true;
        //m_isEventTerrain = true;

        LateInit();
    }
}