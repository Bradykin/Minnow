using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentIceWaterTerrain : GameTerrainBase
{
    public ContentIceWaterTerrain()
    {
        m_damageReduction = Constants.WaterDamageReduction;
        m_costToPass = Constants.WaterMovementCost;
        m_isPassable = false;

        m_name = "IceWater";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isWater = true;
        m_isCold = true;

        LateInit();
    }
}

