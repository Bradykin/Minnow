using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFarmlandBuilding : GameBuildingBase
{
    float m_percentageheal = 15.0f;

    public ContentFarmlandBuilding()
    {
        m_range = 2;
        
        m_name = "Farmland";
        m_desc = $"A safe place to stop and eat, farmland heals all ally units in {m_range} for {m_percentageheal}% of their max health at the start of your turn.";
        m_rarity = GameRarity.Common;
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 25;
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
            GameUnit unit = surroundingTiles[i].m_occupyingUnit;

            if (unit == null)
            {
                continue;
            }

            if (unit.GetTeam() == Team.Enemy)
            {
                continue;
            }

            unit.Heal(Mathf.CeilToInt(unit.GetMaxHealth() * (m_percentageheal / 100.0f)));
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
