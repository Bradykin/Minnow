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

        m_expandsPlaceRange = true;

        LateInit();
    }

    public override void TriggerEndOfWave()
    {
        base.TriggerEndOfWave();

        m_gameTile.GetWorldTile().ExpandPlaceRange(m_sightRange-1);
    }

    protected override void Die()
    {
        m_isDestroyed = true;

        m_gameTile.GetWorldTile().ReducePlaceRange(m_sightRange-1);
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsForest())
        {
            return true;
        }

        return false;
    }
}
