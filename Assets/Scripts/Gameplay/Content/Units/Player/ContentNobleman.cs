using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNobleman : GameUnit
{
    private int m_powerBoost = 8;
    private int m_staminaRegenBoost = 2;

    public ContentNobleman()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        AddKeyword(new GameRangeKeyword(2), true, false);
        AddKeyword(new GameForestwalkKeyword(), true, false);

        m_name = "Nobleman";
        m_desc = "When in a forest, gains: +" + m_powerBoost + "/+0 and " + m_staminaRegenBoost + " Stamina regen.\n";
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

        if (m_gameTile.GetTerrain().IsForest())
        {
            returnPower += m_powerBoost;
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

        if (m_gameTile.GetTerrain().IsForest())
        {
            returnStaminaRegen += m_staminaRegenBoost;
        }

        return returnStaminaRegen;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 15;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 9;
    }
}