using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentColdPlainsTerrain : GameTerrainBase
{
    public ContentColdPlainsTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "ColdPlains";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_isCold = true;

        m_addedEventTerrainType = typeof(ContentColdPlainsRuinsTerrain);

        LateInit();
    }
}
