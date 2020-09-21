using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDirtPlainsTerrain : GameTerrainBase
{
    public ContentDirtPlainsTerrain()
    {
        m_costToPass = 1;

        m_name = "DirtPlains";
        m_desc = "Simple, no changes.";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.white;

        m_isPassable = true;

        m_unburnedTerrainType = typeof(ContentGrassPlainsTerrain);
        m_addedEventType = typeof(ContentDirtPlainsRuinsTerrain);
    }
}
