using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWetlandsTerrain : GameTerrainBase
{
    public ContentWetlandsTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "Wetlands";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isWaterSource = true;

        m_addedEventTerrainType = typeof(ContentWetlandsRuinsTerrain);

        LateInit();
    }
}
