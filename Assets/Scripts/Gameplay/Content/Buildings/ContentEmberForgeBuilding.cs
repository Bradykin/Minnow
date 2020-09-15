﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEmberForgeBuilding : GameBuildingBase
{
    public ContentEmberForgeBuilding()
    {
        m_name = "Ember Forge";
        m_desc = "<b>Permanently</b> lowers max energy by 1 when built, but instantly kills <b>any</b> random entity within range 3 every turn.";
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

        base.EndTurn();

        List<WorldTile> surroundingTiles;
        surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_curTile, 3);

        List<GameEntity> entities = new List<GameEntity>();
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameEntity entity = surroundingTiles[i].GetGameTile().m_occupyingEntity;

            if (entity != null && !GameHelper.IsBossOrElite(entity))
            {
                entities.Add(entity);
            }
        }

        if (entities.Count == 0)
        {
            return;
        }

        GameEntity targetEntity = entities[Random.Range(0, entities.Count)];

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
