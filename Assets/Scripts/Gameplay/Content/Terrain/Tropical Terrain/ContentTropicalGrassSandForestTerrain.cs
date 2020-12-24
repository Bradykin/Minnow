using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTropicalGrassSandForestTerrain : GameTerrainBase
{
    public ContentTropicalGrassSandForestTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "Tropical Grass Sand Forest";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;

        m_burnedTerrainType = typeof(ContentTropicalGrassSandPlainsTerrain);

        LateInit();
    }
}
