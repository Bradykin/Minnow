using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFumarolePlainsTerrain : GameTerrainBase
{
    public ContentFumarolePlainsTerrain()
    {
        m_name = "FumarolePlains";
        m_desc = "Simple, no changes.";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;
        m_costToPass = 1;

        m_isHot = true;

        LateInit();
    }
}
