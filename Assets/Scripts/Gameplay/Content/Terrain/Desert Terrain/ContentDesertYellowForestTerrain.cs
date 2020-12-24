using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowForestTerrain : GameTerrainBase
{
    public ContentDesertYellowForestTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "DesertYellowForest";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;
        m_isHot = true;

        m_burnedTerrainType = typeof(ContentDesertYellowDirtPlainsTerrain);

        LateInit();
    }
}