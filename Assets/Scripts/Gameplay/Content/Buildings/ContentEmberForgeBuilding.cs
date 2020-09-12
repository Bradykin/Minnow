using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEmberForgeBuilding : GameBuildingBase
{
    public ContentEmberForgeBuilding()
    {
        m_name = "Ember Forge";
        m_desc = "A burning pit of fire; <b>permanently</b> lowers max energy by 1 when built, but instantly kills <b>any</b> random entity within range 3 every turn.";
        m_rarity = GameRarity.Rare;

        m_maxHealth = 150;

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
        surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_curTile, 3);

        List<GameEntity> entities = new List<GameEntity>();
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameEntity entity = surroundingTiles[i].GetGameTile().m_occupyingEntity;

            if (entity != null)
            {
                entities.Add(entity);
            }
        }

        if (entities.Count == 0)
        {
            return;
        }

        GameEntity targetEntity = entities[Random.Range(0, entities.Count)];

        UIHelper.CreateWorldElementNotification("The " + m_name + " blasts the " + targetEntity.m_name + ", instantly killing it!", true, targetEntity.m_curTile.m_curTile);
        targetEntity.Die();
    }

    protected override void Die()
    {
        m_isDestroyed = true;
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
