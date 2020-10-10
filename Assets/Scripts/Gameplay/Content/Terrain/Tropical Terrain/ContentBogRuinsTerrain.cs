using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBogRuinsTerrain : GameTerrainBase
{
    public ContentBogRuinsTerrain()
    {
        m_damageReduction = Mathf.Max(Constants.BogDamageReduction, Constants.RuinsDamageReduction);
        m_costToPass = Mathf.Max(Constants.BogMovementCost, Constants.RuinsMovementCost);
        m_staminaRegenLoss = Constants.BogStaminaRegenLoss;

        m_name = "BogRuins";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isEventTerrain = true;

        m_completedEventTerrainType = typeof(ContentBogTerrain);

        LateInit();
    }
}
