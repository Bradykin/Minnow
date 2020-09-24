using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedGrassDunesTerrain : GameTerrainBase
{
    public ContentDesertRedGrassDunesTerrain()
    {
        m_costToPass = 2;

        m_name = "DesertRedGrassDunes";
        m_desc = "Simple, no changes.";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);
        m_color = Color.white;

        m_isPassable = true;
        m_canBurn = false;

        m_isHot = true;

        m_unburnedTerrainType = null;
        //Add event type

        LateInit();
    }
}
