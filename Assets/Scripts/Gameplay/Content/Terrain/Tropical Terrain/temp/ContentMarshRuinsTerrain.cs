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
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isEventTerrain = true;

        m_completedEventTerrainType = typeof(ContentMarshTerrain);

        LateInit();
    }
}
