using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDirtPlainsRuinsTerrain : GameTerrainBase
{
    public ContentDirtPlainsRuinsTerrain()
    {
        m_damageReduction = Mathf.Max(Constants.PlainsDamageReduction, Constants.RuinsDamageReduction);
        m_costToPass = Mathf.Max(Constants.PlainsMovementCost, Constants.RuinsMovementCost);

        m_name = "DirtPlainsRuins";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 3;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);
        m_color = Color.yellow;

        m_isPassable = true;
        m_isEventTerrain = true;

        m_unburnedTerrainType = typeof(ContentGrassPlainsRuinsTerrain);
        m_completedEventType = typeof(ContentDirtPlainsTerrain);

        LateInit();
    }
}
