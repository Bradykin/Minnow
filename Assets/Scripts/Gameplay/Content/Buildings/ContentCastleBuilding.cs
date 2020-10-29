﻿using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCastleBuilding : GameBuildingBase
{
    public ContentCastleBuilding()
    {
        m_name = "Castle";
        m_desc = "This is your home base. Lose this, and it's game over!";
        m_rarity = GameRarity.Starter;
        m_buildingType = BuildingType.Critical;

        m_maxHealth = 100;
        m_cost = new GameWallet(0);

        m_expandsPlaceRange = true;

        LateInit();
    }

    protected override void Die()
    {
        base.Die();

        GameHelper.ReturnToLevelSelectFromLevelScene();
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
