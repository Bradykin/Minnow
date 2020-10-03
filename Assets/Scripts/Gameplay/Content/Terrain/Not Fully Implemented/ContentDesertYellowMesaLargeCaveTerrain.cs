using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowMesaLargeCaveTerrain : GameTerrainBase
{
    public ContentDesertYellowMesaLargeCaveTerrain()
    {
        m_damageReduction = 4;

        m_name = "DesertYellowMesaLargeCave";
        m_desc = "Impassable.\nUnits on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = Random.Range(1, 3);

        m_isPassable = false;
        m_costToPass = 2;

        m_isMountain = true;
        m_isHot = true;
        m_isCave = true;

        LateInit();
    }
}
