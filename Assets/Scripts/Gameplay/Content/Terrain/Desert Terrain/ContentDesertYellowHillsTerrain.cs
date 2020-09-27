using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowHillsTerrain : GameTerrainBase
{
    public ContentDesertYellowHillsTerrain()
    {
        m_name = "DesertYellowHills";
        m_desc = "2 AP movement.";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;
        m_costToPass = 2;

        m_isHill = true;
        m_isHot = true;

        LateInit();
    }
}
