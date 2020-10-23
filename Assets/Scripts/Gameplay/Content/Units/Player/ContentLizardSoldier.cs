using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLizardSoldier : GameUnit
{
    public ContentLizardSoldier()
    {
        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        AddKeyword(new GameWaterwalkKeyword(), false);

        m_name = "Lizard Soldier";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        SetUnitLevel(GetUnitLevel());

        LateInit();
    }

    public override void SetUnitLevel(int level)
    {
        base.SetUnitLevel(level);

        m_maxHealth = 9;
        m_maxStamina = 5;
        m_staminaRegen = 5;
        m_power = 7;

        if (m_unitLevel >= 1)
        {
            m_staminaRegen = 6;
            m_maxStamina = 6;
        }

        if (m_unitLevel >= 2)
        {
            m_power = 22;
        }
    }
}