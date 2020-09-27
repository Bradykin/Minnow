using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarshTerrain : GameTerrainBase
{
    public ContentMarshTerrain()
    {
        m_name = "Marsh";
        m_desc = "Simple, no changes.";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;
        m_costToPass = 1;

        m_isWater = true;

        LateInit();
    }
}
