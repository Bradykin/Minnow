using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDirtPlainsRuinsTerrain : GameTerrainBase
{
    public ContentDirtPlainsRuinsTerrain()
    {
        m_costToPass = 2;

        m_name = "DirtPlainsRuins";
        m_desc = "2 AP movement.";
        m_maxTerrainImageNumber = 3;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.yellow;

        m_isPassable = true;
        m_isEventTerrain = true;

        m_unburnedTerrainType = typeof(ContentGrassPlainsRuinsTerrain);
        m_completedEventType = typeof(ContentDirtPlainsTerrain);
    }
}
