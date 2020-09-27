using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedHillsPondTerrain : GameTerrainBase
{
    public ContentDesertRedHillsPondTerrain()
    {
        m_rangeModifier = 1;

        m_name = "DesertRedHillsPond";
        m_desc = "3 AP movement.\nRanged entities on this tile get +" + m_rangeModifier + " increased range.";
        m_terrainImageNumber = Random.Range(1, 3);

        m_isPassable = true;
        m_costToPass = 3;

        m_isHill = true;
        m_isHot = true;
        m_isWater = true;

        LateInit();
    }
}
