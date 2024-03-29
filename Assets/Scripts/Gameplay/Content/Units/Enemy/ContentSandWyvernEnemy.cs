﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSandWyvernEnemy : GameEnemyUnit
{
    public ContentSandWyvernEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 65;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_attack = 25;
        m_attackSFX = AudioHelper.BirdFlap;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Sand Wyvern";
        m_desc = "";

        AddKeyword(new GameFlyingKeyword(), true, false);
        AddKeyword(new GameVictoriousKeyword(new GameGainStaminaAction(this, m_maxStamina)), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameVictoriousKeyword(new GameGainStaminaAction(this, m_maxStamina)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}