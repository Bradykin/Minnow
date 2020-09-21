using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentScrublandPlainsRuinsTerrain : GameTerrainBase
{
    public ContentScrublandPlainsRuinsTerrain()
    {
        m_costToPass = 1;

        m_name = "ScrublandPlainsRuins";
        m_desc = "Simple, no changes.";
        m_maxTerrainImageNumber = 3;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.white;

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentDirtPlainsRuinsTerrain);
        m_completedEventType = typeof(ContentScrublandPlainsTerrain);
    }
}
