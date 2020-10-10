using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentColdDirtPlainsTerrain : GameTerrainBase
{
    public ContentColdDirtPlainsTerrain()
    {
        m_damageReduction = Constants.PlainsDamageReduction;
        m_costToPass = Constants.PlainsMovementCost;

        m_name = "ColdDirtPlains";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_isCold = true;

        m_addedEventTerrainType = typeof(ContentColdDirtPlainsRuinsTerrain);

        LateInit();
    }
}
