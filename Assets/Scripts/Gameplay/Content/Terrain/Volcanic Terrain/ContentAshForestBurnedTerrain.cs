using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAshForestBurnedTerrain : GameTerrainBase
{
    public ContentAshForestBurnedTerrain()
    {
        m_name = "AshForestBurned";
        m_desc = "2 Stamina movement.\nUnits on this tile take " + m_damageReduction + " less damage.";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;
        m_costToPass = 2;

        m_isForest = true;
        m_isHot = true;
        m_isBurned = true;

        LateInit();
    }
}