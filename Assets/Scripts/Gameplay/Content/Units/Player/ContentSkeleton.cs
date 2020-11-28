using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSkeleton : GameUnit
{
    private int m_chance;
    private int m_healthBonus;

    public ContentSkeleton()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_chance = 75;
        m_healthBonus = 15;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;
        AddKeyword(new GameRegenerateKeyword(5), true, false);

        m_name = "Skeleton";
        m_desc = m_chance + "% chance to survive a fatal hit with 1 health.  If it does, it <b>permanently</b> gains " + m_healthBonus + " max health.\n";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 5;
        m_maxStamina = 4;
        m_staminaRegen = 2;
        m_power = 3;
    }

    protected override bool ShouldRevive(out int healthSurvivedAt)
    {
        bool shouldReviveBase =  base.ShouldRevive(out healthSurvivedAt);
        bool shouldReviveTrigger = GameHelper.PercentChanceRoll(m_chance);

        bool isReviving = shouldReviveBase || shouldReviveTrigger;

        if (isReviving)
        {
            this.AddStats(0, m_healthBonus, true, true);
        }

        return isReviving;
    }
}