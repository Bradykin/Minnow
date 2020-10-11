﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLichEnemy : GameEnemyUnit
{
    public ContentLichEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BuffBoss))
        {
            m_maxHealth = 700;
            m_maxStamina = 8;
            m_staminaRegen = 8;
            m_power = 30;
        }
        else
        {
            m_maxHealth = 350;
            m_maxStamina = 8;
            m_staminaRegen = 6;
            m_power = 15;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Event;
        m_isBoss = true;

        m_minWave = Constants.FinalWaveNum;
        m_maxWave = Constants.FinalWaveNum;

        m_name = "Lich";
        m_desc = "The final boss.  Kill it, and win.";

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(3));
        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void Die()
    {
        WorldController.Instance.WinGame();

        base.Die();
    }
}