using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedMountainTerrain : GameTerrainBase
{
    public ContentDesertRedMountainTerrain()
    {
        m_damageReduction = 4;

        m_name = "DesertRedMountain";
        m_desc = "Impassable.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = false;
        m_costToPass = 2;

        m_isMountain = true;
        m_isHot = true;

        LateInit();
    }
}
