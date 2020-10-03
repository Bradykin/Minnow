﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHomonculus : GameUnit
{
    private int m_effectAmount = 1;
    private int m_effectRange = 1;

    public ContentHomonculus()
    {
        m_maxHealth = 4;
        m_maxStamina = 6;
        m_staminaRegen = 2;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Homonculus";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconEntity(m_name);

        m_keywordHolder.m_keywords.Add(new GameKnowledgeableKeyword(new GameGainStaminaRangeAction(this, m_effectAmount, m_effectRange)));

        LateInit();
    }
}