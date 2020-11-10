using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowForestRuinsTerrain : GameTerrainBase
{
    public ContentDesertYellowForestRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "DesertYellowForestRuins";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;
        m_isHot = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentDesertYellowDirtPlainsRuinsTerrain);
        m_completedEventTerrainType = typeof(ContentDesertYellowForestTerrain);

        LateInit();
    }
}