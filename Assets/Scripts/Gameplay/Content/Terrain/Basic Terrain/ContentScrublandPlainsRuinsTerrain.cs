using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentScrublandPlainsRuinsTerrain : GameTerrainBase
{
    public ContentScrublandPlainsRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "ScrublandPlainsRuins";
        m_maxTerrainImageNumber = 3;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber);

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentDirtPlainsRuinsTerrain);
        m_completedEventTerrainType = typeof(ContentScrublandPlainsTerrain);

        LateInit();
    }
}
