using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFarmBuilding : GameBuildingBase
{
    public int m_healingNum = 6;

    public ContentFarmBuilding()
    {
        m_name = "Farm";
        m_desc = "Provide food for your troops, healing all allied entities in the tiles around it for " + m_healingNum + " at the end of the turn.";
        m_rarity = GameRarity.Common;

        m_maxHealth = 5;

        m_expandsPlaceRange = false;

        LateInit();
    }

    public override void EndTurn()
    {
        if (m_isDestroyed)
        {
            return;
        }

        List<WorldTile> surroundingTiles;
        surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_curTile, 1);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameEntity entity = surroundingTiles[i].GetGameTile().m_occupyingEntity;

            if (entity == null)
            {
                continue;
            }

            if (entity.GetTeam() == Team.Enemy)
            {
                continue;
            }

            UIHelper.CreateWorldElementNotification("The " + m_name + " heals the " + entity.m_name + " for " + m_healingNum + " health!", true, surroundingTiles[i]);
            entity.Heal(m_healingNum);
        }
    }

    protected override void Die()
    {
        m_isDestroyed = true;
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain)
    {
        if (terrain is ContentGrassTerrain)
        {
            return true;
        }

        return false;
    }
}
