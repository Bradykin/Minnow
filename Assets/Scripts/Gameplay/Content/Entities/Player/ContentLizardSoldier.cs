using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLizardSoldier : GameUnit
{
    public ContentLizardSoldier()
    {
        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_keywordHolder.m_keywords.Add(new GameWaterwalkKeyword());

        m_name = "Lizard Soldier";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        SetUnitLevel(GetUnitLevel());

        LateInit();
    }

    public override void SetUnitLevel(int level)
    {
        base.SetUnitLevel(level);

        m_maxHealth = 4;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 2;

        if (m_unitLevel >= 1)
        {
            m_maxHealth = 15;
            m_staminaRegen = 4;
        }

        if (m_unitLevel >= 2)
        {
            m_power = 10;
        }
    }
}