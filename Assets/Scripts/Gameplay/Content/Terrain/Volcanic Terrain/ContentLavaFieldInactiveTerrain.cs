using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLavaFieldInactiveTerrain : GameTerrainBase
{
    public ContentLavaFieldInactiveTerrain()
    {
        m_damageReduction = Constants.PlainsDamageReduction;
        m_costToPass = Constants.PlainsMovementCost;

        m_name = "LavaFieldInactive";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isHot = true;

        LateInit();
    }
}
