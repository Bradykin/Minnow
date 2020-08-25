using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrassTerrain : GameTerrainBase
{
    public ContentGrassTerrain()
    {
        m_name = "Grasslands";
        m_icon = UIHelper.GetIconTerrain(m_name);
        m_color = Color.white;

        m_isPassable = true;
        m_costToPass = 1;
    }
}
