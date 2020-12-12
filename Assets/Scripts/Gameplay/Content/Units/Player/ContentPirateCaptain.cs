using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPirateCaptain : GameUnit
{
    private int m_statBoost = 50;
    private int m_regenVal = 10;

    public ContentPirateCaptain()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        AddKeyword(new GameWaterwalkKeyword(), true, false);

        m_name = "Pirate Captain";
        m_desc = $"Gets +{m_statBoost}/+{m_statBoost} and <b>Regen</b> {m_regenVal} when on water.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override int GetPower()
    {
        int returnPower = base.GetPower();

        if (m_gameTile == null)
        {
            return returnPower;
        }

        if (m_gameTile.GetTerrain().IsWater())
        {
            returnPower += m_statBoost;
        }

        return returnPower;
    }

    public override int GetMaxHealth()
    {
        int returnHealth = base.GetMaxHealth();

        if (m_gameTile == null)
        {
            return returnHealth;
        }

        if (m_gameTile.GetTerrain().IsWater())
        {
            returnHealth += m_statBoost;
        }

        return returnHealth;
    }

    public override GameRegenerateKeyword GetRegenerateKeyword()
    {
        GameRegenerateKeyword toReturn = new GameRegenerateKeyword(0);

        if (base.GetRegenerateKeyword() != null)
        {
            toReturn.AddKeyword(base.GetRegenerateKeyword());
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            if (m_gameTile.GetTerrain().IsWater())
            {
                toReturn.AddKeyword(new GameRegenerateKeyword(m_regenVal));
            }
        }

        if (toReturn.m_regenVal == 0)
        {
            toReturn = null;
        }

        return toReturn;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 8;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 0;
    }
}