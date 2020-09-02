using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class ContentFortressBuilding : GameBuildingBase
{
    public int m_power = 5;

    public ContentFortressBuilding()
    {
        m_name = "Fortress";
        m_desc = "Shoots at enemies on tiles in a range of 2 with " + m_power + " power at the start of your turn.";

        m_maxHealth = 40;
        m_rarity = GameRarity.Common;

        LateInit();
    }

    public override void EndTurn()
    {
        if (m_isDestroyed)
        {
            return;
        }

        List<WorldTile> surroundingTiles;
        surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_curTile, 2);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameEntity entity = surroundingTiles[i].GetGameTile().m_occupyingEntity;

            if (entity == null)
            {
                continue;
            }

            if (entity.GetTeam() == Team.Player)
            {
                continue;
            }

            UIHelper.CreateWorldElementNotification("The " + m_name + " shoots the " + entity.m_name + " for " + m_power + " damage!", true, surroundingTiles[i]);
            entity.GetHit(m_power);
        }
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
