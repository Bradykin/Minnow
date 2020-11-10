using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTropicalPlainsTerrain : GameTerrainBase
{
    public ContentTropicalPlainsTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "TropicalPlains";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = true;

        m_burnedTerrainType = typeof(ContentDirtPlainsTerrain);
        m_addedEventTerrainType = typeof(ContentTropicalPlainsRuinsTerrain);
        m_marshTideRiseTerrainType = typeof(ContentMarshTerrain);

        LateInit();
    }
}
