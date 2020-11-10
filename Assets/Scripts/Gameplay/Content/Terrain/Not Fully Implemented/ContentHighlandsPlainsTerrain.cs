using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHighlandsPlainsTerrain : GameTerrainBase
{
    public ContentHighlandsPlainsTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "HighlandsPlains";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;

        LateInit();
    }
}
