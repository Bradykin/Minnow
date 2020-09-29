using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedForestRuinsTerrain : GameTerrainBase
{
    public ContentDesertRedForestRuinsTerrain()
    {
        m_damageReduction = Constants.ForestDamageReduction;
        m_costToPass = Constants.ForestMovementCost;

        m_name = "DesertRedForestRuins";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_isForest = true;
        m_canBurn = true;
        m_isHot = true;
        m_isEventTerrain = true;

        m_burnedTerrainType = typeof(ContentDesertRedDirtPlainsRuinsTerrain);
        m_completedEventType = typeof(ContentDesertRedForestTerrain);

        LateInit();
    }
}