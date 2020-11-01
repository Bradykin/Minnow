using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentWizardTowerBuilding : GameBuildingBase
{
    public int m_power = 5;
    public int m_spellPowerMultiplier = 20;

    public ContentWizardTowerBuilding()
    {
        m_range = 5;

        m_name = "Wizard Tower";
        m_desc = "Damage a random enemy unit in a range of " + m_range + " for " + m_power + ", plus " + m_spellPowerMultiplier + " times your amount of spellpower at the start of your turn.";
        m_rarity = GameRarity.Rare;
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 35;
        m_cost = new GameWallet(120);
        m_sightRange = m_range;

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

        List<GameTile> surroundingTiles = surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, m_range, 0).Where(t => t.IsOccupied() && t.m_occupyingUnit.GetTeam() == Team.Enemy).ToList();

        if (surroundingTiles.Count == 0)
        {
            return;
        }

        int highestHealthAmount = surroundingTiles.Max(t => t.m_occupyingUnit.GetCurHealth());
        GameTile highestHealthTile = surroundingTiles.FirstOrDefault(t => t.m_occupyingUnit.GetCurHealth() == highestHealthAmount);

        GamePlayer player = GameHelper.GetPlayer();
        int spellPower = 0;

        if (player != null)
        {
            spellPower = player.GetSpellPower();
        }

        highestHealthTile.m_occupyingUnit.GetHit(m_power + spellPower * m_spellPowerMultiplier);
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsHill())
        {
            return true;
        }

        return false;
    }
}
