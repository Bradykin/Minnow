using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPowerCrystalBuilding : GameBuildingBase
{
    public ContentPowerCrystalBuilding()
    {
        m_name = "Power Crystal";
        m_desc = "While this is alive, the boss is invulnerable.";
        m_rarity = GameRarity.Common;
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 75;

        m_expandsPlaceRange = false;
        m_cost = new GameWallet(0);

        LateInit();

        m_team = Team.Enemy;
    }

    public override void Die()
    {
        base.Die();

        WorldController.Instance.m_gameController.m_map.DestroyCrystal();
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        return true;
    }
}
