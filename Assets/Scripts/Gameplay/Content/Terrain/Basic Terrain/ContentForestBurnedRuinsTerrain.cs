using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForestBurnedRuinsTerrain : GameTerrainBase
{
    public ContentForestBurnedRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "ForestBurnedRuins";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_isForest = true;
        m_isHot = true;
        m_isBurned = true;
        m_isEventTerrain = true;

        m_unburnedTerrainType = typeof(ContentForestRuinsTerrain);
        m_completedEventTerrainType = typeof(ContentForestBurnedTerrain);

        LateInit();
    }
}

