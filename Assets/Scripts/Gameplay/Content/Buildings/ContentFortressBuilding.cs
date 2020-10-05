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
        m_desc = "Damage enemy units on tiles in a range of " + m_range + " for " + m_power + " at the start of your turn.";
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 40;
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

        List<GameTile> surroundingTiles = surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_gameTile, m_range, 0);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameUnit unit = surroundingTiles[i].m_occupyingUnit;

            if (unit == null)
            {
                continue;
            }

            if (unit.GetTeam() == Team.Player)
            {
                continue;
            }

            unit.GetHit(m_power);
        }
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain)
    {
        if (terrain.IsMountain())
        {
            return true;
        }

        return false;
    }
}
