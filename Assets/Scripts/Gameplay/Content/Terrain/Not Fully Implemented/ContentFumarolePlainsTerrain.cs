using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFumarolePlainsTerrain : GameTerrainBase
{
    public ContentFumarolePlainsTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "Fumarole Plains";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;

        m_isHot = true;

        LateInit();
    }
}
