using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowDirtDunesTerrain : GameTerrainBase
{
    public ContentDesertYellowDirtDunesTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "DesertYellowDirtDunes";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isHot = true;

        LateInit();
    }
}
