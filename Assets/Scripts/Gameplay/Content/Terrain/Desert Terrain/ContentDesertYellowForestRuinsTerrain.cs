using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowForestRuinsTerrain : GameTerrainBase
{
    public ContentDesertYellowForestRuinsTerrain()
    {
        m_damageReduction = Constants.ForestDamageReduction;
        m_costToPass = Constants.ForestMovementCost;

        m_name = "DesertYellowForestRuins";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;
        m_isHot = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentDesertYellowDirtPlainsRuinsTerrain);
        m_completedEventType = typeof(ContentDesertYellowForestTerrain);

        LateInit();
    }
}