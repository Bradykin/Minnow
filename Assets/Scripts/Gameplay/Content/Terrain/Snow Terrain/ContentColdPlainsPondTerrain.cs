using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentColdPlainsPondTerrain : GameTerrainBase
{
    public ContentColdPlainsPondTerrain()
    {
        m_name = "ColdPlains";
        m_desc = "Simple, no changes.";
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_costToPass = 1;

        m_isCold = true;
        m_isWater = true;

        LateInit();
    }
}
