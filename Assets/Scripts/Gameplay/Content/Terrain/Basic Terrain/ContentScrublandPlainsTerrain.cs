using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentScrublandPlainsTerrain : GameTerrainBase
{
    public ContentScrublandPlainsTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "Scrubland Plains";
        m_maxTerrainImageNumber = 5;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber);

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = true;

        m_burnedTerrainType = typeof(ContentDirtPlainsTerrain);

        LateInit();
    }
}
