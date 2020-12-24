using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTundraPlainsTerrain : GameTerrainBase
{
    public ContentTundraPlainsTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "TundraPlains";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;

        LateInit();
    }
}
