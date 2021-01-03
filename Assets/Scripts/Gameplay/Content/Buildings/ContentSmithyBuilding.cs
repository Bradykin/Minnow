﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSmithyBuilding : GameBuildingBase
{
    public ContentSmithyBuilding()
    {
        m_range = 0;

        m_name = "Smithy";
        m_desc = "Gain a bonus intermission action at the end of each wave.";
        m_rarity = GameRarity.Common;
        m_buildingType = BuildingType.Economic;

        m_maxHealth = 25;
        m_cost = new GameWallet(100);

        m_expandsPlaceRange = false;

        LateInit();
    }

    public override void TriggerEndOfWave()
    {
        if (!m_isDestroyed)
        {
            GamePlayer player = GameHelper.GetPlayer();

            if (player == null)
            {
                return;
            }

            player.AddBonusActions(1);
        }

        base.TriggerEndOfWave();
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
