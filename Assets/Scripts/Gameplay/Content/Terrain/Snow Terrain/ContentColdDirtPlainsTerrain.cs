using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentColdDirtPlainsTerrain : GameTerrainBase
{
    public ContentColdDirtPlainsTerrain()
    {
        m_name = "ColdDirtPlains";
        m_desc = "Simple, no changes.";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;
        m_costToPass = 1;

        m_isCold = true;

        LateInit();
    }
}
