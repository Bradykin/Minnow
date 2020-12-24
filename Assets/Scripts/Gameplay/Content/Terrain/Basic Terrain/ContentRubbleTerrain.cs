using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRubbleTerrain : GameTerrainBase
{
    public ContentRubbleTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "Rubble";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;

        LateInit();
    }
}
