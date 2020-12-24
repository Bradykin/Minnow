using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLavaFieldActiveTerrain : GameTerrainBase
{
    public ContentLavaFieldActiveTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "Lava Field Active";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isHot = true;
        m_isLava = true;

        LateInit();
    }
}
