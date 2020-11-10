using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowDirtPlainsRuinsTerrain : GameTerrainBase
{
    public ContentDesertYellowDirtPlainsRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "DesertYellowDirtPlainsRuins";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = false;
        m_isHot = true;
        m_isEventTerrain = true;

        m_completedEventTerrainType = typeof(ContentDesertYellowDirtPlainsTerrain);

        LateInit();
    }
}
