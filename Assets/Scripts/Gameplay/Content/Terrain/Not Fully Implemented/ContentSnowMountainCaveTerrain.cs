using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowMountainCaveTerrain : GameTerrainBase
{
    public ContentSnowMountainCaveTerrain()
    {
        m_damageReduction = 4;

        m_name = "SnowMountainCave";
        m_desc = "Impassable.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = Random.Range(1, 5);
        m_color = Color.red;

        m_isPassable = false;
        m_costToPass = 2;

        m_isMountain = true;
        m_isCold = true;
        m_isCave = true;

        LateInit();
    }
}
