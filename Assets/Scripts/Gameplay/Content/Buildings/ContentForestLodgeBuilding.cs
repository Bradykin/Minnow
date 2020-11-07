using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForestLodgeBuilding : GameBuildingBase
{
    public ContentForestLodgeBuilding()
    {
        m_name = "Forest Lodge";
        m_desc = "Expand the range you can summon units around the lodge!.";
        m_rarity = GameRarity.Common;
        m_buildingType = BuildingType.Economic;

        m_maxHealth = 30;
        m_cost = new GameWallet(60);

        m_expandsPlaceRange = true;

        LateInit();
    }

    public override void TriggerEndOfWave()
    {
        base.TriggerEndOfWave();

        m_gameTile.GetWorldTile().ExpandPlaceRange(2);
    }

    public override void Die()
    {
        base.Die();

        m_gameTile.GetWorldTile().ReducePlaceRange(2);
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsForest() && !terrain.IsBurned())
        {
            return true;
        }

        return false;
    }
}
