using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWetlandsRuinsTerrain : GameTerrainBase
{
    public ContentWetlandsRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "WetlandsRuins";
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isWaterSource = true;
        m_isEventTerrain = true;

        m_completedEventTerrainType = typeof(ContentWetlandsTerrain);

        LateInit();
    }
}
