using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertDunesTerrain : GameTerrainBase
{
    public ContentDesertDunesTerrain()
    {
        m_damageReduction = Constants.DunesDamageReduction;
        m_costToPass = Constants.DunesMovementCost;

        m_name = "DesertDunes";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(0, m_maxTerrainImageNumber + 1); ;

        m_isPassable = true;
        m_canBurn = false;
        m_isHot = true;

        LateInit();
    }
}
