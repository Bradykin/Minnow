using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentIceCrackedTerrain : GameTerrainBase
{
    public ContentIceCrackedTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "IceCracked";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isIce = true;
        m_isIceCracked = true;

        m_iceCrackedTerrainType = typeof(ContentIceWaterTerrain);

        LateInit();
    }
}
