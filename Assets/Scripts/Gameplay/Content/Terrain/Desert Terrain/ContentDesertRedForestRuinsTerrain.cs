using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedForestRuinsTerrain : GameTerrainBase
{
    public ContentDesertRedForestRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "DesertRedForestRuins";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;
        m_isHot = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentDesertRedDirtPlainsRuinsTerrain);
        m_completedEventTerrainType = typeof(ContentDesertRedForestTerrain);

        LateInit();
    }
}