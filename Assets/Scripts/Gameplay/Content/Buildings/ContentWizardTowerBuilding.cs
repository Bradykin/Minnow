using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentWizardTowerBuilding : GameBuildingBase
{
    private int m_attackBase = 10;
    private int m_spellcraftStacks = 0;
    private int m_spellcraftStacksIncreaseAmount = 2;

    public ContentWizardTowerBuilding()
    {
        m_range = 3;

        m_name = "Wizard Tower";
        m_rarity = GameRarity.Rare;
        m_buildingType = BuildingType.Defensive;

        m_maxHealth = 35;
        m_cost = new GameWallet(65);
        m_sightRange = m_range;

        m_expandsPlaceRange = false;
        m_spellcraftBuilding = true;

        LateInit();
    }

    public override string GetDesc()
    {
        if (GetGameTile() == null)
        {
            m_desc = $"Damage a random enemy unit in a range {m_range} for {m_attackBase + m_spellcraftStacks} at the start of your turn.\n<b>Spellcraft</b>: <b>Permanently</b> increase the damage this tower does by {m_spellcraftStacksIncreaseAmount}.";
        }
        else
        {
            m_desc = $"Current Damage: {m_attackBase + m_spellcraftStacks}\nDamage a random enemy unit in a range {m_range} for {m_attackBase + m_spellcraftStacks} at the start of your turn.\n<b>Spellcraft</b>: <b>Permanently</b> increase the damage this tower does by {m_spellcraftStacksIncreaseAmount}.";
        }

        return m_desc;
    }

    public override void OnSpellCraft()
    {
        m_spellcraftStacks += m_spellcraftStacksIncreaseAmount;
        UIHelper.CreateWorldElementNotification($"Tower gained {m_spellcraftStacksIncreaseAmount} damage!", true, GetWorldTile().gameObject);
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

        highestHealthTile.GetOccupyingUnit().GetHitByAbility(m_attackBase + m_spellcraftStacks);
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

    //============================================================================================================//

    public override JsonGameBuildingData SaveToJson()
    {
        JsonGameBuildingData jsonData = new JsonGameBuildingData
        {
            name = m_name,
            curHealth = m_curHealth,
            isDestroyed = m_isDestroyed,
            intValue = m_spellcraftStacks
        };

        return jsonData;
    }

    public override string SaveToJsonAsString()
    {
        JsonGameBuildingData jsonData = new JsonGameBuildingData
        {
            name = m_name,
            curHealth = m_curHealth,
            isDestroyed = m_isDestroyed,
            intValue = m_spellcraftStacks
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonGameBuildingData jsonData)
    {
        m_curHealth = jsonData.curHealth;
        m_isDestroyed = jsonData.isDestroyed;
        m_spellcraftStacks = jsonData.intValue;
    }
}
