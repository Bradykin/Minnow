using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrassTerrain : GameTerrainBase
{
    public ContentGrassTerrain()
    {
        m_name = "Grasslands";
        m_desc = "When there isn't a war going on; these meadows are peaceful!";
        m_icon = null;
        m_color = Color.white;

        m_isPassable = true;
        m_costToPass = 1;
    }
}
