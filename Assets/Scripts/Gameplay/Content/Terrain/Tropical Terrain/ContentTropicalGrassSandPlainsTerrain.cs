using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTropicalGrassSandPlainsTerrain : GameTerrainBase
{
    public ContentTropicalGrassSandPlainsTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "TropicalGrassSandPlains";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;

        m_addedEventTerrainType = typeof(ContentTropicalGrassSandPlainsRuinsTerrain);

        LateInit();
    }
}
