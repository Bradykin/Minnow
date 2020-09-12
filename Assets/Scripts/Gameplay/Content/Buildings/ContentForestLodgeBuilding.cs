using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForestLodgeBuilding : GameBuildingBase
{
    public ContentForestLodgeBuilding()
    {
        m_name = "Forest Lodge";
        m_desc = "Expand the range you can summon entities around the lodge!.";
        m_rarity = GameRarity.Common;

        m_maxHealth = 30;

        m_expandsPlaceRange = true;

        LateInit();
    }

    public override void StartTurn()
    {
        if (m_isDestroyed)
        {
            return;
        }

        base.StartTurn();

        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.DrawCard();
    }

    public override void TriggerEndOfWave()
    {
        base.TriggerEndOfWave();

        m_curTile.ExpandPlaceRange(m_sightRange);
    }

    protected override void Die()
    {
        m_isDestroyed = true;

        m_curTile.ReducePlaceRange(m_sightRange);
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain)
    {
        if (terrain.IsForest())
        {
            return true;
        }

        return false;
    }
}
