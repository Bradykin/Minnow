using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSwampForestRuinsTerrain : GameTerrainBase
{
    public ContentSwampForestRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "SwampForestRuins";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentForestBurnedRuinsTerrain);
        m_completedEventTerrainType = typeof(ContentSwampForestTerrain);

        LateInit();
    }
}
