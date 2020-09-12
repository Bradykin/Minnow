﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrcShamanEnemy : GameEnemyEntity
{
    public ContentOrcShamanEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 4;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 1;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Orc Shaman";
        m_desc = "ORC SHAMAN DESC";

        LateInit();
    }
}