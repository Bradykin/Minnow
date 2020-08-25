using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForestTerrain : GameTerrainBase
{
    public ContentForestTerrain()
    {
        m_damageReduction = 2;

        m_name = "Forest";
        m_desc = "Entities on this tile take " + m_damageReduction + " less damage.";
        m_icon = UIHelper.GetIconTerrain(m_name);
        m_color = Color.green;

        m_isPassable = true;
        m_costToPass = 2;
    }
}
