using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTropicalPlainsRuinsTerrain : GameTerrainBase
{
    public ContentTropicalPlainsRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "TropicalPlainsRuins";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentDirtPlainsRuinsTerrain);
        m_completedEventTerrainType = typeof(ContentTropicalPlainsTerrain);
        m_marshTideRiseTerrainType = typeof(ContentMarshRuinsTerrain);

        LateInit();
    }
}
