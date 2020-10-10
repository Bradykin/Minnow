using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowMountainTerrain : GameTerrainBase
{
    public ContentSnowMountainTerrain()
    {
        m_damageReduction = Constants.MountainsDamageReduction;
        m_costToPass = Constants.MountainsMovementCost;
        m_isPassable = false;

        m_name = "SnowMountain";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isMountain = true;
        m_isCold = true;

        LateInit();
    }
}
