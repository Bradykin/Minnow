using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class ContentFortressBuilding : GameBuildingBase
{
    public int m_power = 6;

    public ContentFortressBuilding()
    {
        m_range = 3;

        m_name = "Fortress";
        m_desc = "Damage enemy units in Range " + m_range + " for " + m_power + " damage at the start of your turn.";
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 40;
        m_cost = new GameWallet(70);
        m_rarity = GameRarity.Common;

        LateInit();
    }

    public override void StartTurn()
    {
        if (m_isDestroyed)
        {
            return;
        }

        base.EndTurn();

        List<GameTile> surroundingTiles = surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, m_range, 0);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

            if (unit == null)
            {
                continue;
            }

            if (unit.GetTeam() == Team.Player)
            {
                continue;
            }

            unit.GetHitByAbility(m_power);
            AudioHelper.PlaySFX(AudioHelper.BowHeavy);
        }
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsMountain() && !terrain.IsVolcano())
        {
            return true;
        }

        return false;
    }
}
