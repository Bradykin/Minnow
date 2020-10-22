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

        m_maxHealth = 40;

        m_expandsPlaceRange = false;

        LateInit();

        m_team = Team.Enemy;
    }

    protected override void Die()
    {
        WorldController.Instance.m_gameController.m_map.DestroyCrystal();
        m_isDestroyed = true;
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        return true;
    }
}
