using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadowWarlock : GameEntity
{
    public ContentShadowWarlock()
    {
        m_maxHealth = 12;
        m_maxAP = 5;
        m_apRegen = 3;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        m_keywordHolder.m_keywords.Add(new GameSpellcraftKeyword(new GameGainPowerAction(this, 1)));
        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(2));

        m_name = "Shadow Warlock";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}