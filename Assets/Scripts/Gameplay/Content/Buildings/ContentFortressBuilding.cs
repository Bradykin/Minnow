using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class ContentFortressBuilding : GameBuildingBase
{
    public int m_power = 6;

    public ContentFortressBuilding()
    {
        m_name = "Fortress";
        m_desc = "Shoots at enemy units on tiles in a range of 2 with " + m_power + " power at the start of your turn.";
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

        List<GameTile> surroundingTiles = surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_gameTile, 2);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameEntity entity = surroundingTiles[i].m_occupyingEntity;

            if (entity == null)
            {
                continue;
            }

            if (entity.GetTeam() == Team.Player)
            {
                continue;
            }

            entity.GetHit(m_power);
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
