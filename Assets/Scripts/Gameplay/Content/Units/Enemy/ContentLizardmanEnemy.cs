﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For AI:
//Favours staying in or near water
//Does a move-attack-move
public class ContentLizardmanEnemy : GameEnemyUnit
{
    public ContentLizardmanEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 13;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_attack = 13;
        m_attackSFX = AudioHelper.SpearHeavy;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Lizardman";
        m_desc = "Lizardmen move on water tiles without spending Stamina.\n";

        AddKeyword(new GameWaterwalkKeyword(), true, false);
        m_instantWaterMovement = true;
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameGainStatsAction(this, 2, 0)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AILizardmanChooseTargetToAttackStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AILizardmanFleeToWaterStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}