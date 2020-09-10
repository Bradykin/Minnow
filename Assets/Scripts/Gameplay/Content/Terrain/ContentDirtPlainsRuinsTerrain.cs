using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDirtPlainsRuinsTerrain : GameTerrainBase
{
    public ContentDirtPlainsRuinsTerrain()
    {
        m_name = "DirtPlainsRuins";
        m_desc = "2 AP movement.";
        m_terrainImageNumber = Random.Range(1, 4);
        m_icon = UIHelper.GetIconTerrain(m_name, m_terrainImageNumber);
        m_color = Color.yellow;

        m_isPassable = true;
        m_costToPass = 2;
    }
}
