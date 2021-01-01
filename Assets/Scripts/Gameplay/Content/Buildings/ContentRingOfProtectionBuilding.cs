using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRingOfProtectionBuilding : GameBuildingBase
{
    public ContentRingOfProtectionBuilding()
    {
        m_range = 0;

        m_name = "Ring of Protection";
        m_desc = "The spirits guard units that enter this place, granting units a damage shield every time they enter.";
        m_rarity = GameRarity.Uncommon;
        m_buildingType = BuildingType.Economic;

        m_maxHealth = 12;
        m_cost = new GameWallet(50);

        m_expandsPlaceRange = false;

        LateInit();
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsPlains())
        {
            return true;
        }

        return false;
    }
}