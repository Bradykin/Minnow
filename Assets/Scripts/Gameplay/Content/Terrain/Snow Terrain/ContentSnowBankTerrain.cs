using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowBankTerrain : GameTerrainBase
{
    public ContentSnowBankTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "Snow Bank";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isHill = true;
        m_isSnow = true;

        LateInit();
    }
}
