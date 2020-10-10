using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTundraPlainsPondTerrain : GameTerrainBase
{
    public ContentTundraPlainsPondTerrain()
    {
        m_damageReduction = Constants.PlainsDamageReduction;
        m_costToPass = Constants.PlainsMovementCost;

        m_name = "TundraPlainsPond";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_isCold = true;
        m_isWaterSource = true;

        LateInit();
    }
}
