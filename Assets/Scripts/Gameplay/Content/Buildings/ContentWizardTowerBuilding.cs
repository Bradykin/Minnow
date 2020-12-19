using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentWizardTowerBuilding : GameBuildingBase
{
    public int m_power = 5;
    public int m_magicPowerMultiplier = 20;

    public ContentWizardTowerBuilding()
    {
        m_range = 4;

        m_name = "Wizard Tower";
        m_rarity = GameRarity.Rare;
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 35;
        m_cost = new GameWallet(100);
        m_sightRange = m_range;

        m_expandsPlaceRange = false;

        LateInit();
    }

    public override string GetDesc()
    {
        m_desc = "Damage a random enemy unit in a range of " + m_range + " for " + m_power + ", plus " + m_magicPowerMultiplier + " times your amount of <b>Magic Power</b>(+" + (m_magicPowerMultiplier * GameHelper.GetPlayer().GetMagicPower()) + ") at the start of your turn.";

        return m_desc;
    }

    public override void StartTurn()
    {
        if (m_isDestroyed)
        {
            return;
        }

        base.EndTurn();

        List<GameTile> surroundingTiles = surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, m_range, 0).Where(t => t.IsOccupied() && t.GetOccupyingUnit().GetTeam() == Team.Enemy).ToList();

        if (surroundingTiles.Count == 0)
        {
            return;
        }

        int highestHealthAmount = surroundingTiles.Max(t => t.GetOccupyingUnit().GetCurHealth());
        GameTile highestHealthTile = surroundingTiles.FirstOrDefault(t => t.GetOccupyingUnit().GetCurHealth() == highestHealthAmount);

        GamePlayer player = GameHelper.GetPlayer();
        int magicPower = 0;

        if (player != null)
        {
            magicPower = player.GetMagicPower();
        }

        highestHealthTile.GetOccupyingUnit().GetHitByAbility(m_power + magicPower * m_magicPowerMultiplier);
        AudioHelper.PlaySFX(AudioHelper.SpellAttackMedium);
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
