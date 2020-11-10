using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowForestRuinsTerrain : GameTerrainBase
{
    public ContentSnowForestRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "SnowForestRuins";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_isForest = true;
        m_isCold = true;
        m_canBurn = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentForestBurnedRuinsTerrain);
        m_completedEventTerrainType = typeof(ContentSnowForestTerrain);

        LateInit();
    }
}
