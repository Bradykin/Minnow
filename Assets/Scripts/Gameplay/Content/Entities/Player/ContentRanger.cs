using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRanger : GameEntity
{
    private int m_powerBoost;
    private int m_rangeBoost;
    private int m_apRegenBoost;

    public ContentRanger()
    {
        m_powerBoost = 4;
        m_rangeBoost = 1;
        m_apRegenBoost = 1;

        m_maxHealth = 8;
        m_maxAP = 5;
        m_apRegen = 2;
        m_power = 5;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Ranger";
        m_desc = "When in a forest, gains: " + m_powerBoost + " power, " + m_rangeBoost + " range, " + m_apRegenBoost + " ap regen.";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

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

    public override int GetAPRegen()
    {
        int returnAPRegen = base.GetAPRegen();

        if (m_gameTile == null)
        {
            return returnAPRegen;
        }

        if (m_gameTile.GetTerrain().IsForest())
        {
            returnAPRegen += m_apRegenBoost;
        }

        return returnAPRegen;
    }

    public override int GetRange()
    {
        int returnRange = base.GetRange();

        if (m_gameTile == null)
        {
            return returnRange;
        }

        if (m_gameTile.GetTerrain().IsForest())
        {
            returnRange += m_rangeBoost;
        }

        return returnRange;
    }
}