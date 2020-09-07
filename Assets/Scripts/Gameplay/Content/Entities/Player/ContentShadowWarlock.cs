using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadowWarlock : GameEntity
{
    public ContentShadowWarlock()
    {
        m_maxHealth = 12;
        m_maxAP = 5;
        m_apRegen = 2;
        m_power = 2;
        m_sightRange = 3;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        m_keywordHolder.m_keywords.Add(new GameSpellcraftKeyword(new GameGainPowerAction(this, 1)));
        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(2));

        m_name = "Shadow Warlock";
        m_desc = "Has sight range of 3";
        m_typeline = Typeline.Mystic;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}