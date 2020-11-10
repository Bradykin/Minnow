using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDirtPlainsRuinsTerrain : GameTerrainBase
{
    public ContentDirtPlainsRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "DirtPlainsRuins";
        m_maxTerrainImageNumber = 3;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_isEventTerrain = true;

        m_unburnedTerrainType = typeof(ContentGrassPlainsRuinsTerrain);
        m_completedEventTerrainType = typeof(ContentDirtPlainsTerrain);

        LateInit();
    }
}
