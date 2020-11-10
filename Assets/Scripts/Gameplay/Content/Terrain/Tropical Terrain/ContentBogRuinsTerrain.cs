using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBogRuinsTerrain : GameTerrainBase
{
    public ContentBogRuinsTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;
        //m_staminaRegenLoss = Constants.BogStaminaRegenLoss; Alex - look at this

        m_name = "BogRuins";
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isEventTerrain = true;

        m_completedEventTerrainType = typeof(ContentBogTerrain);
        m_marshTideRiseTerrainType = typeof(ContentOceanCalmTerrain);
        m_marshTideLowerTerrainType = typeof(ContentMarshRuinsTerrain);

        LateInit();
    }
}
