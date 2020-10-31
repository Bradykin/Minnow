using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMineBuilding : GameBuildingBase
{
    public ContentMineBuilding()
    {
        m_name = "Mine";
        m_desc = "Mines boost production, giving an extra draw every turn.";
        m_rarity = GameRarity.Uncommon;
        m_buildingType = BuildingType.Economic;

        m_maxHealth = 15;
        m_cost = new GameWallet(75);

        m_expandsPlaceRange = false;

        LateInit();
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsMountain())
        {
            return true;
        }

        return false;
    }
}
