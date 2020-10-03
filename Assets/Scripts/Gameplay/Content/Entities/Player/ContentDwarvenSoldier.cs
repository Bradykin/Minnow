using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarvenSoldier : GameUnit
{
    public ContentDwarvenSoldier()
    {
        m_maxHealth = 8;
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_power = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Dwarven Soldier";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}
