﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrcEnemy : GameEnemyUnit
{
    public ContentOrcEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 22;
        m_maxStamina = 6;
        m_staminaRegen = 3;
        m_power = 6;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 3;
        m_maxWave = 4;

        m_name = "Orc";
        m_desc = "";

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit));
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit));
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit));
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit));

        LateInit();
    }
}