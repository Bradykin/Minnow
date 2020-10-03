using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadowWarlock : GameUnit
{
    public ContentShadowWarlock()
    {
        m_maxHealth = 12;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        m_keywordHolder.m_keywords.Add(new GameSpellcraftKeyword(new GameGainPowerAction(this, 1)));
        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(2));

        m_name = "Shadow Warlock";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}