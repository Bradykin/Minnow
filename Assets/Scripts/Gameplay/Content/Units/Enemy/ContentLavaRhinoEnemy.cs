﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLavaRhinoEnemy : GameEnemyUnit
{
    public ContentLavaRhinoEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 90;
        m_maxStamina = 4;
        m_staminaRegen = 2;
        m_power = 100;
        m_staminaToAttack = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Rare;
        m_shouldAlwaysPassEnemies = true;

        m_name = "Lava Rhino";
        m_desc = "Can move through your units.\nWill only attack at full Stamina, and only buildings.\nCan hit buildings that have a unit on top of them.\n";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameFlyingKeyword(), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AILavaRhinoScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AILavaRhinoChooseTargetToAttackStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AILavaRhinoMoveToTargetStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AILavaRhinoAttackUntilOutOfStaminaStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}