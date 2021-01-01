using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class ContentHillFortBuilding : GameBuildingBase
{
    public ContentHillFortBuilding()
    {
        m_range = 0;
        
        m_name = "Hill Fort";
        m_desc = "Ranged units positioned on this building gain an additional +1 range.";
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 30;
        m_cost = new GameWallet(55);
        m_rarity = GameRarity.Common;

        LateInit();
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsHill())
        {
            return true;
        }

        return false;
    }
}
