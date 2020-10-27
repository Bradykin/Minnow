using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarvenSoldier : GameUnit
{
    public ContentDwarvenSoldier()
    {
        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Dwarven Soldier";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        InitializeWithLevel(GetUnitLevel());

        LateInit();
    }

    public override void InitializeWithLevel(int level)
    {
        m_maxHealth = 8;
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_power = 4;

        if (level >= 1)
        {
            m_power = 7;
        }

        if (level >= 2)
        {
            m_maxHealth = 15;
        }
    }
}
