using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRubbleTerrain : GameTerrainBase
{
    public ContentRubbleTerrain()
    {
        m_damageReduction = Constants.RuinsDamageReduction;
        m_costToPass = Constants.RuinsMovementCost;

        m_name = "Rubble";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;

        m_addedEventTerrainType = typeof(ContentDirtPlainsRuinsTerrain);

        LateInit();
    }
}
