using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTropicalSandForestRuinsTerrain : GameTerrainBase
{
    public ContentTropicalSandForestRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "TropicalSandForestRuins";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentTropicalSandPlainsRuinsTerrain);
        m_completedEventTerrainType = typeof(ContentTropicalSandForestTerrain);

        LateInit();
    }
}
