using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEmberForgeBuilding : GameBuildingBase
{
    public ContentEmberForgeBuilding()
    {
        m_name = "Ember Forge";
        m_desc = "Instantly kills <b>1</b> random non-elite unit within range 3 every turn. (Allied or enemy).";
        m_rarity = GameRarity.Rare;
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 90;

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

        List<GameTile> surroundingTiles;
        surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_gameTile, 3);

        List<GameUnit> entities = new List<GameUnit>();
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameUnit entity = surroundingTiles[i].m_occupyingUnit;

            if (entity != null && !GameHelper.IsBossOrElite(entity))
            {
                entities.Add(entity);
            }
        }

        if (entities.Count == 0)
        {
            return;
        }

        GameUnit targetEntity = entities[Random.Range(0, entities.Count)];

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
