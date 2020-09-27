using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedGrassPlainsPondTerrain : GameTerrainBase
{
    public ContentDesertRedGrassPlainsPondTerrain()
    {
        m_name = "DesertRedGrassPlainsPond";
        m_desc = "Simple, no changes.";
        m_terrainImageNumber = Random.Range(1, 3);

        m_isPassable = true;
        m_costToPass = 1;

        m_isHot = true;

        LateInit();
    }
}
