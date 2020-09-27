using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedForestTerrain : GameTerrainBase
{
    public ContentDesertRedForestTerrain()
    {
        m_damageReduction = 2;

        m_name = "DesertRedForest";
        m_desc = "2 AP movement.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;
        m_costToPass = 2;

        m_isForest = true;
        m_isHot = true;

        LateInit();
    }
}
