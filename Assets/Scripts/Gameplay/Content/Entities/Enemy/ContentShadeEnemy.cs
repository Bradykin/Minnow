﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadeEnemy : GameEnemyEntity
{
    public ContentShadeEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 7;
        m_maxAP = 6;
        m_apRegen = 4;
        m_power = 4;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Shade";
        m_desc = "Yep, it flies.";

        m_minWave = 5;

        LateInit();

        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());
    }
}