using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWetlandsRuinsTerrain : GameTerrainBase
{
    public ContentWetlandsRuinsTerrain()
    {
        m_damageReduction = Mathf.Max(Constants.MarshDamageReduction, Constants.RuinsDamageReduction);
        m_costToPass = Mathf.Max(Constants.MarshMovementCost, Constants.RuinsMovementCost);

        m_name = "WetlandsRuins";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isWaterSource = true;
        m_isEventTerrain = true;

        m_completedEventTerrainType = typeof(ContentWetlandsTerrain);

        LateInit();
    }
}
