using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowSaltFlatsTerrain : GameTerrainBase
{
    public ContentDesertYellowSaltFlatsTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "Desert Yellow Salt Flats";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_canBurn = false;
        m_isHot = true;
        //m_isEventTerrain = true;

        LateInit();
    }
}