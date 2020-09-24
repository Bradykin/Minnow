using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedMountainCaveTerrain : GameTerrainBase
{
    public ContentDesertRedMountainCaveTerrain()
    {
        m_damageReduction = 4;

        m_name = "DesertRedMountainCave";
        m_desc = "Impassable.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = Random.Range(1, 5);
        m_color = Color.red;

        m_isPassable = false;
        m_costToPass = 2;

        m_isMountain = true;
        m_isHot = true;
        m_isCave = true;

        LateInit();
    }
}
