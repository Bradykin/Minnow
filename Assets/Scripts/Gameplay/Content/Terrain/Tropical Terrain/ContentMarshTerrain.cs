using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarshTerrain : GameTerrainBase
{
    public ContentMarshTerrain()
    {
        m_damageReduction = Constants.MarshDamageReduction;
        m_costToPass = Constants.MarshMovementCost;
        m_staminaRegenLoss = Constants.MarshStaminaRegenLoss;

        m_name = "Marsh";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPlains = true;
        m_isPassable = true;
        m_isWaterSource = true;

        m_addedEventTerrainType = typeof(ContentMarshRuinsTerrain);
        m_marshTideRiseTerrainType = typeof(ContentBogTerrain);
        m_marshTideLowerTerrainType = typeof(ContentTropicalPlainsTerrain);

        LateInit();
    }
}
