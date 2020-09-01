﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMineBuilding : GameBuildingBase
{
    public ContentMineBuilding()
    {
        m_name = "Mine";
        m_desc = "Mines boost production, giving an extra draw every turn.";
        m_rarity = GameRarity.Uncommon;

        m_maxHealth = 15;

        m_expandsPlaceRange = false;

        LateInit();
    }

    protected override void Die()
    {
        m_isDestroyed = true;
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain)
    {
        if (terrain is ContentMountainTerrain)
        {
            return true;
        }

        return false;
    }
}
