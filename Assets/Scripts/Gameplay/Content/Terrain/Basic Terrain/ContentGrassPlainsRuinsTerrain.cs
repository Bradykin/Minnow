using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrassPlainsRuinsTerrain : GameTerrainBase
{
    public ContentGrassPlainsRuinsTerrain()
    {
        m_damageReduction = Mathf.Max(Constants.PlainsDamageReduction, Constants.RuinsDamageReduction);
        m_costToPass = Mathf.Max(Constants.PlainsMovementCost, Constants.RuinsMovementCost);

        m_name = "GrassPlainsRuins";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 3;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentDirtPlainsRuinsTerrain);
        m_completedEventType = typeof(ContentGrassPlainsTerrain);

        LateInit();
    }
}
