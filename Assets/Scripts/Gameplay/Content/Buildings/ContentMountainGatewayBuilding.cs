using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class ContentMountainGatewayBuilding : GameBuildingBase
{
    public ContentMountainGatewayBuilding()
    {
        m_range = 0;

        m_name = "Mountain Gateway";
        m_desc = "Your units can pas through this tile.";
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 30;
        m_cost = new GameWallet(45);
        m_rarity = GameRarity.Common;

        LateInit();
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsMountain() && !terrain.IsVolcano())
        {
            return true;
        }

        return false;
    }
}
