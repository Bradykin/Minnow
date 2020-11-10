using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedGrassPlainsRuinsTerrain : GameTerrainBase
{
    public ContentDesertRedGrassPlainsRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "DesertRedGrassPlainsRuins";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = true;
        m_isHot = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentDesertRedDirtPlainsRuinsTerrain);
        m_completedEventTerrainType = typeof(ContentDesertRedGrassPlainsTerrain);

        LateInit();
    }
}
