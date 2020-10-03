using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFishOracle : GameUnit
{
    public ContentFishOracle()
    {
        m_maxHealth = 8;
        m_maxStamina = 4;
        m_staminaRegen = 2;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        m_keywordHolder.m_keywords.Add(new GameSpellcraftKeyword(new GameDrawCardAction(1)));

        m_name = "Fish Oracle";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}
