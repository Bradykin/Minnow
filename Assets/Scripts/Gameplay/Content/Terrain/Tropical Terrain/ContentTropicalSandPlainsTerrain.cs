using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTropicalSandPlainsTerrain : GameTerrainBase
{
    public ContentTropicalSandPlainsTerrain()
    {
        m_damageReduction = Constants.PlainsDamageReduction;
        m_costToPass = Constants.PlainsMovementCost;

        m_name = "TropicalSandPlains";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;

        m_addedEventTerrainType = typeof(ContentTropicalSandPlainsRuinsTerrain);

        LateInit();
    }
}
