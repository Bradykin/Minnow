﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrcShamanEnemy : GameEnemyEntity
{
    public ContentOrcShamanEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 8;
        m_maxAP = 6;
        m_apRegen = 3;
        m_power = 4;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Rare;

        m_name = "Orc Shaman";
        m_desc = "Magical blasts away...";

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(2));
        m_keywordHolder.m_keywords.Add(new GameSpellcraftKeyword(new GameGainPowerAction(this, 2)));

        LateInit();
    }
}