using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonSoldier : GameUnit
{
    public ContentDemonSoldier()
    {
        m_maxHealth = 40;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 6;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());

        m_name = "Demon Soldier";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}