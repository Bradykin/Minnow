using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLizardSoldier : GameUnit
{
    public ContentLizardSoldier()
    {
        m_maxHealth = 4;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_keywordHolder.m_keywords.Add(new GameWaterwalkKeyword());

        m_name = "Lizard Soldier";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}