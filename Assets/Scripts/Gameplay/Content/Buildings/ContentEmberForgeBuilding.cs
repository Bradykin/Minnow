using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEmberForgeBuilding : GameBuildingBase
{
    public ContentEmberForgeBuilding()
    {
        m_range = 3;

        m_name = "Ember Forge";
        m_desc = "Instantly kills <b>1</b> random non-elite unit within range " + m_range + " every turn. (Allied or enemy).";
        m_rarity = GameRarity.Rare;
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 90;
        m_cost = new GameWallet(250);

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
        surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, m_range, 0);

        List<GameUnit> units = new List<GameUnit>();
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameUnit unit = surroundingTiles[i].m_occupyingUnit;

            if (unit != null && !GameHelper.IsBossOrElite(unit))
            {
                units.Add(unit);
            }
        }

        if (units.Count == 0)
        {
            return;
        }

        GameUnit targetUnit = units[Random.Range(0, units.Count)];

        targetUnit.Die();
    }

    protected override void Die()
    {
        m_isDestroyed = true;
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsMountain())
        {
            return true;
        }

        return false;
    }
}
