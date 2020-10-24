using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarshRuinsTerrain : GameTerrainBase
{
    public ContentMarshRuinsTerrain()
    {
        m_damageReduction = Mathf.Max(Constants.MarshDamageReduction, Constants.RuinsDamageReduction);
        m_costToPass = Mathf.Max(Constants.MarshMovementCost, Constants.RuinsMovementCost);
        m_staminaRegenLoss = Constants.MarshStaminaRegenLoss;

        m_name = "MarshRuins";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPlains = true;
        m_isPassable = true;
        m_isWaterSource = true;
        m_isEventTerrain = true;

        m_completedEventTerrainType = typeof(ContentMarshTerrain);
        m_marshTideRiseTerrainType = typeof(ContentBogRuinsTerrain);
        m_marshTideLowerTerrainType = typeof(ContentTropicalPlainsRuinsTerrain);

        LateInit();
    }
}
