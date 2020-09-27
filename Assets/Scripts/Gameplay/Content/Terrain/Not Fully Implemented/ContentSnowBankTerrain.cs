using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowBankTerrain : GameTerrainBase
{
    public ContentSnowBankTerrain()
    {
        m_damageReduction = 2;

        m_name = "SnowBank";
        m_desc = "2 AP movement.";
        m_terrainImageNumber = Random.Range(1, 5);

        m_isPassable = true;
        m_costToPass = 2;

        m_isForest = true;
        m_isCold = true;

        LateInit();
    }
}
