using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentScrublandPlainsTerrain : GameTerrainBase
{
    public ContentScrublandPlainsTerrain()
    {
        m_damageReduction = Constants.PlainsDamageReduction;
        m_costToPass = Constants.PlainsMovementCost;

        m_name = "ScrublandPlains";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 5;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber);

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = true;

        m_burnedTerrainType = typeof(ContentDirtPlainsTerrain);
        m_addedEventType = typeof(ContentScrublandPlainsRuinsTerrain);

        LateInit();
    }
}
