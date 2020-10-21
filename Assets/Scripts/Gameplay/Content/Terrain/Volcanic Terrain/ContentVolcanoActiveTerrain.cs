using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoActiveTerrain : GameTerrainBase
{
    public ContentVolcanoActiveTerrain()
    {
        m_damageReduction = Constants.MountainsDamageReduction;
        m_costToPass = Constants.MountainsMovementCost;
        m_isPassable = false;

        m_name = "VolcanoActive";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isMountain = true;
        m_isHot = true;
        m_isVolcano = true;

        LateInit();
    }
}
