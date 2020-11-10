using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBogTerrain : GameTerrainBase
{
    public ContentBogTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Difficult;
        //m_staminaRegenLoss = Constants.BogStaminaRegenLoss;

        m_name = "Bog";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;

        m_addedEventTerrainType = typeof(ContentBogRuinsTerrain);
        m_marshTideRiseTerrainType = typeof(ContentOceanCalmTerrain);
        m_marshTideLowerTerrainType = typeof(ContentMarshTerrain);

        LateInit();
    }
}
