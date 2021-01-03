using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFarmlandBuilding : GameBuildingBase
{
    private int m_heal = 20;

    public ContentFarmlandBuilding()
    {
        m_range = 2;
        
        m_name = "Farmland";
        m_desc = $"Farmlands heals all ally units in Range {m_range} for {m_heal} at the start of your turn, but does not provide Cover.";
        m_rarity = GameRarity.Common;
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 15;
        m_cost = new GameWallet(55);

        m_expandsPlaceRange = false;

        LateInit();
    }

    public override void StartTurn()
    {
        if (m_isDestroyed)
        {
            return;
        }

        base.EndTurn();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, m_range, 0);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

            if (unit == null)
            {
                continue;
            }

            if (unit.GetTeam() == Team.Enemy)
            {
                continue;
            }

            unit.Heal(m_heal);
        }
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsPlains())
        {
            return true;
        }

        return false;
    }
}
