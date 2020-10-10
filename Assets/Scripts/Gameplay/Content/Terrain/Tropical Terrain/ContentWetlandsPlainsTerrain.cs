using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWetlandsPlainsTerrain : GameTerrainBase
{
    public ContentWetlandsPlainsTerrain()
    {
        m_damageReduction = Constants.PlainsDamageReduction;
        m_costToPass = Constants.PlainsMovementCost;

        m_name = "WetlandsPlains";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_isWaterSource = true;

        m_addedEventTerrainType = typeof(ContentWetlandsPlainsRuinsTerrain);

        LateInit();
    }
}
