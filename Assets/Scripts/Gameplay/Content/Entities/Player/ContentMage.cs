using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMage : GameUnit
{
    public ContentMage()
    {
        m_maxHealth = 6;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(3));
        m_keywordHolder.m_keywords.Add(new GameKnowledgeableKeyword(new GameGainPowerAction(this, 3)));

        m_name = "Mage";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}