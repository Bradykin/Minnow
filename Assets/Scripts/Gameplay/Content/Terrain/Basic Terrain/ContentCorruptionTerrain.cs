using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCorruptionTerrain : GameTerrainBase
{
    public ContentCorruptionTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "Corruption";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;

        m_isCorruption = true;

        LateInit();
    }
}
