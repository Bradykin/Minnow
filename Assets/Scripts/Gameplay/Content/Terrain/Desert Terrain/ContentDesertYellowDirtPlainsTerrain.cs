using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowDirtPlainsTerrain : GameTerrainBase
{
    public ContentDesertYellowDirtPlainsTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "DesertYellowDirtPlains";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = false;
        m_isHot = true;

        m_addedEventTerrainType = typeof(ContentDesertYellowDirtPlainsRuinsTerrain);

        LateInit();
    }
}
