﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Priority order: adjacent units, castle, buildings, other units
public class ContentMobolaEnemy : GameEnemyUnit
{
    public ContentMobolaEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 85;
        m_maxStamina = 8;
        m_staminaRegen = 2;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Mobola";
        m_desc = "";

        m_minWave = 5;
        m_maxWave = 6;

        m_keywordHolder.m_keywords.Add(new GameEnrageKeyword(new GameGainPowerAction(this, 3)));
        m_keywordHolder.m_keywords.Add(new GameMomentumKeyword(new GameGainPowerAction(this, 3)));
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_keywordHolder.m_keywords.Add(new GameRegenerateKeyword(15));
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit));
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit));
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit));
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit));

        LateInit();
    }
}