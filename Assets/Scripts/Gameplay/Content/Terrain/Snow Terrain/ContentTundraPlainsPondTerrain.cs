using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTundraPlainsPondTerrain : GameTerrainBase
{
    public ContentTundraPlainsPondTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "TundraPlainsPond";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_isCold = true;

        LateInit();
    }
}
