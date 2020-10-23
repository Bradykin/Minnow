using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTempleBuilding : GameBuildingBase
{
    public ContentTempleBuilding()
    {
        m_name = "Temple";
        m_desc = "Faith in the gods grants you an extra energy every turn this isn't destroyed.";
        m_rarity = GameRarity.Uncommon;
        m_buildingType = BuildingType.Economic;

        m_maxHealth = 10;
        m_cost = new GameWallet(130);

        m_expandsPlaceRange = false;

        LateInit();
    }

    protected override void Die()
    {
        m_isDestroyed = true;
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsFlatTerrain())
        {
            return true;
        }

        return false;
    }
}
