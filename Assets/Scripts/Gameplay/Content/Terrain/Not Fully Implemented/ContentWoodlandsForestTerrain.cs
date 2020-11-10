using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWoodlandsForestTerrain : GameTerrainBase
{
    public ContentWoodlandsForestTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "WoodlandsForest";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;

        m_isForest = true;

        LateInit();
    }
}
