using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBogTerrain : GameTerrainBase
{
    public ContentBogTerrain()
    {
        m_name = "Bog";
        m_desc = "2 AP movement.";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;
        m_costToPass = 2;

        m_isWater = true;

        LateInit();
    }
}
