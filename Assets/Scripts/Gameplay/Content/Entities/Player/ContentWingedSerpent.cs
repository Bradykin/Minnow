using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWingedSerpent : GameUnit
{
    public ContentWingedSerpent()
    {
        m_maxHealth = 3;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());

        m_name = "Winged Serpent";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}