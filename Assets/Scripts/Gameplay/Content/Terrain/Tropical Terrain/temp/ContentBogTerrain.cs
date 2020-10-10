using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBogTerrain : GameTerrainBase
{
    public ContentBogTerrain()
    {
        m_damageReduction = Constants.BogDamageReduction;
        m_costToPass = Constants.BogMovementCost;
        m_staminaRegenLoss = Constants.BogStaminaRegenLoss;

        m_name = "Bog";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;

        m_addedEventTerrainType = typeof(ContentBogRuinsTerrain);

        LateInit();
    }
}
