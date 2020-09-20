using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHomonculus : GameEntity
{
    private int m_effectAmount = 1;
    private int m_effectRange = 1;

    public ContentHomonculus()
    {
        m_maxHealth = 4;
        m_maxAP = 6;
        m_apRegen = 2;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Homonculus";
        m_desc = "On Knowledgeable, give one action point to itself and allies within 1 range.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconEntity(m_name);

        m_keywordHolder.m_keywords.Add(new GameKnowledgeableKeyword(new GameGainAPRangeAction(this, m_effectAmount, m_effectRange)));

        LateInit();
    }
}