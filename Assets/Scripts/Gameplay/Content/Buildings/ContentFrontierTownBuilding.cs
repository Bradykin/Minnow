using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrontierTownBuilding : GameBuildingBase
{
    public ContentFrontierTownBuilding()
    {
        m_range = 0;

        m_name = "Frontier Town";
        m_desc = "A base for explorers on the frontier. Very durable to enemy attacks.";
        m_rarity = GameRarity.Common;
        m_buildingType = BuildingType.Wall;

        m_maxHealth = 40;
        m_cost = new GameWallet(50);

        m_range = 0;

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
