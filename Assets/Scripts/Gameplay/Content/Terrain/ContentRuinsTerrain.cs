using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRuinsTerrain : GameTerrainBase
{
    public ContentRuinsTerrain()
    {
        m_damageReduction = 2;

        m_name = "Ruins";
        m_desc = "2 AP movement.\nEntities on this tile take " + m_damageReduction + " less damage.";
        m_icon = UIHelper.GetIconTerrain(m_name);
        m_color = Color.yellow;

        m_isPassable = true;
        m_costToPass = 2;
    }
}
