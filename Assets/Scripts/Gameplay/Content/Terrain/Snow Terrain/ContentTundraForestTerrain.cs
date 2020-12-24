using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTundraForestTerrain : GameTerrainBase
{
    public ContentTundraForestTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "Tundra Forest";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isForest = true;
        m_isSnow = true;
        m_canBurn = true;

        m_burnedTerrainType = typeof(ContentForestBurnedTerrain);

        LateInit();
    }
}
