using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedForestPondTerrain : GameTerrainBase
{
    public ContentDesertRedForestPondTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "DesertRedForestPond";
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;
        m_isHot = true;

        LateInit();
    }
}