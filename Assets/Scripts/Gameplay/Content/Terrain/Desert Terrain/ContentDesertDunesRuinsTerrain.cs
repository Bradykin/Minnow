using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertDunesRuinsTerrain : GameTerrainBase
{
    public ContentDesertDunesRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "DesertDunesRuins";
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_canBurn = false;
        m_isHot = true;
        m_isEventTerrain = true;

        m_completedEventTerrainType = typeof(ContentDesertDunesTerrain);

        LateInit();
    }
}
