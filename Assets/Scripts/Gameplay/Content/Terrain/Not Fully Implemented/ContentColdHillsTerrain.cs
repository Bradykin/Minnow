using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentColdHillsTerrain : GameTerrainBase
{
    public ContentColdHillsTerrain()
    {
        m_rangeModifier = 1;

        m_name = "ColdHills";
        m_desc = "3 Stamina movement.\nRanged entities on this tile get +" + m_rangeModifier + " increased range.";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;
        m_costToPass = 3;

        m_isHill = true;
        m_isCold = true;

        LateInit();
    }
}
