using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWingedSerpent : GameUnit
{
    public ContentWingedSerpent()
    {
        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        AddKeyword(new GameFlyingKeyword(), false);

        m_name = "Winged Serpent";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        SetUnitLevel(GetUnitLevel());

        LateInit();
    }

    public override void SetUnitLevel(int level)
    {
        base.SetUnitLevel(level);

        m_maxHealth = 3;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 1;

        if (m_unitLevel >= 1)
        {
            m_maxHealth = 8;
        }

        if (m_unitLevel >= 2)
        {
            m_staminaRegen = 3;
        }
    }
}