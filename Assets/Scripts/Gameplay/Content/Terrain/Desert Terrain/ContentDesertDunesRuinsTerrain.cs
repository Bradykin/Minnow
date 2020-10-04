using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertDunesRuinsTerrain : GameTerrainBase
{
    public ContentDesertDunesRuinsTerrain()
    {
        m_damageReduction = Mathf.Max(Constants.PlainsDamageReduction, Constants.RuinsDamageReduction);
        m_costToPass = Mathf.Max(Constants.PlainsMovementCost, Constants.RuinsMovementCost);

        m_name = "DesertDunesRuins";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(0, m_maxTerrainImageNumber + 1); ;

        m_isPassable = true;
        m_canBurn = false;
        m_isHot = true;
        m_isEventTerrain = true;

        LateInit();
    }
}
