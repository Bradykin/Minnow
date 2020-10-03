using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowRuinsTerrain : GameTerrainBase
{
    public ContentSnowRuinsTerrain()
    {
        m_name = "SnowRuins";
        m_desc = "2 Stamina movement.";
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_costToPass = 2;

        m_isCold = true;
        m_isEventTerrain = true;

        m_completedEventType = typeof(ContentSnowPlainsTerrain);

        LateInit();
    }
}
