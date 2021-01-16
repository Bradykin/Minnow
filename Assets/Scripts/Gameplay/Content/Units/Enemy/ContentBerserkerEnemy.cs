﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBerserkerEnemy : GameEnemyUnit
{
    public ContentBerserkerEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 6;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_attack = 5;
        m_attackSFX = AudioHelper.SwordHeavy;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Berserker";
        
        m_desc = "";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDamageReductionKeyword(2), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}