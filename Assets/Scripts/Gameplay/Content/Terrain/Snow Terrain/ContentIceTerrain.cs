using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentIceTerrain : GameTerrainBase
{
    public ContentIceTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "Ice";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isIce = true;

        m_iceCrackedTerrainType = typeof(ContentIceCrackedTerrain);

        LateInit();
    }
}
