using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWetlandsPlainsTerrain : GameTerrainBase
{
    public ContentWetlandsPlainsTerrain()
    {
        m_name = "WetlandsPlains";
        m_desc = "Simple, no changes.";
        m_terrainImageNumber = Random.Range(1, 5);
        m_color = Color.white;

        m_isPassable = true;
        m_costToPass = 1;

        m_isWater = true;

        LateInit();
    }
}
