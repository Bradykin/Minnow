using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSkeleton : GameUnit
{
    private int m_chance;
    private int m_healthBonus;

    public ContentSkeleton()
    {
        m_chance = 75;
        m_healthBonus = 15;

        m_maxHealth = 5;
        m_maxStamina = 4;
        m_staminaRegen = 2;
        m_power = 3;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;
        m_keywordHolder.m_keywords.Add(new GameRegenerateKeyword(5));

        m_name = "Skeleton";
        m_desc = m_chance + "% chance to survive a fatal hit with 1 health.  If it does, it gains " + m_healthBonus + " max health.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    protected override bool ShouldRevive()
    {
        bool shouldReviveBase =  base.ShouldRevive();
        bool shouldReviveTrigger = GameHelper.PercentChanceRoll(m_chance);

        bool isReviving = shouldReviveBase || shouldReviveTrigger;

        if (isReviving)
        {
            this.AddMaxHealth(m_healthBonus);
        }

        return isReviving;
    }
}