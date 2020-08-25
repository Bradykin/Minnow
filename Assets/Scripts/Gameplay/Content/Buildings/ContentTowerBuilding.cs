using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class ContentTowerBuilding : GameBuildingBase
{
    public int m_power = 5;

    public ContentTowerBuilding()
    {
        m_name = "Tower";
        m_desc = "Shoots at enemies on all surrounding tiles (but not this tile) with " + m_power + " power at the start of your turn.";

        m_maxHealth = 40;
        m_rarity = GameRarity.Common;

        LateInit();
    }

    public override void EndTurn()
    {
        List<WorldTile> surroundingTiles;
        surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_curTile, 1);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameEntity entity = surroundingTiles[i].m_gameTile.m_occupyingEntity;

            if (entity == null)
            {
                continue;
            }

            if (entity.GetTeam() == Team.Player)
            {
                continue;
            }

            UIHelper.CreateWorldElementNotification("The " + m_name + " shoots the " + entity.m_name + " for " + m_power + " damage!", true, surroundingTiles[i]);
            entity.Hit(m_power);
        }
    }
}
