using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowMesaLargePondTerrain : GameTerrainBase
{
    public ContentDesertYellowMesaLargePondTerrain()
    {
        m_damageReduction = Constants.MountainsDamageReduction;
        m_costToPass = Constants.MountainsMovementCost;
        m_isPassable = false;

        m_name = "DesertYellowMesaLargePond";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isMountain = true;
        m_isHot = true;
        m_isWaterSource = true;

        LateInit();
    }
}
