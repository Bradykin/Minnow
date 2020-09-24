using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowDirtDunesTerrain : GameTerrainBase
{
    public ContentDesertYellowDirtDunesTerrain()
    {
        m_name = "DesertYellowDirtDunes";
        m_desc = "Simple, no changes.";
        m_terrainImageNumber = Random.Range(1, 5);
        m_color = Color.yellow;

        m_isPassable = true;
        m_costToPass = 1;

        m_isHot = true;

        LateInit();
    }
}
