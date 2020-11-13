using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGroundskeeper : GameUnit
{
    private int m_powerBoost = 5;
    private int m_staminaRegenBoost = 2;

    public ContentGroundskeeper()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.3f, 0);

        m_maxHealth = 50;
        m_maxStamina = 4;
        m_staminaRegen = 1;
        m_power = 5;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Groundskeeper";
        m_desc = "Gains +" + m_powerBoost + "/+0 and +" + m_staminaRegenBoost + " Stamina regen while in a forest.\n";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameForestwalkKeyword(), false);

        LateInit();
    }

    public override int GetPower()
    {
        int returnPower = base.GetPower();

        if (m_gameTile == null)
        {
            return returnPower;
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            if (m_gameTile.GetTerrain().IsForest())
            {
                returnPower += m_powerBoost;
            }
        }

        return returnPower;
    }

    public override int GetStaminaRegen()
    {
        int returnStaminaRegen = base.GetStaminaRegen();

        if (m_gameTile == null)
        {
            return returnStaminaRegen;
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            if (m_gameTile.GetTerrain().IsForest())
            {
                returnStaminaRegen += m_staminaRegenBoost;
            }
        }

        return returnStaminaRegen;
    }
}