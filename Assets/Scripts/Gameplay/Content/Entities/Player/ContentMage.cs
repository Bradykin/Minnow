using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMage : GameEntity
{
    public ContentMage()
    {
        m_maxHealth = 3;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(3));
        m_keywordHolder.m_keywords.Add(new GameKnowledgeableKeyword(new GameGainPowerAction(this, 1)));

        m_name = "Mage";
        m_typeline = Typeline.Mystic;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}