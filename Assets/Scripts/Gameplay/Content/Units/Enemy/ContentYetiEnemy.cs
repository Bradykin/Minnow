﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Refuse to stay out of fog of war????
//If no fog of war near player targets that are closish to them, head to the mountains?
public class ContentYetiEnemy : GameEnemyUnit
{
    public ContentYetiEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 35;
        m_maxStamina = 4;
        m_staminaRegen = 2;
        m_power = 9;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Yeti";
        m_desc = "";

        m_minWave = 5;
        m_maxWave = 6;

        AddKeyword(new GameRangeKeyword(4), false);
        AddKeyword(new GameMountainwalkKeyword(), false);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameGainStatsAction(this, 3, 0)), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIYetiChooseTargetToAttackStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}