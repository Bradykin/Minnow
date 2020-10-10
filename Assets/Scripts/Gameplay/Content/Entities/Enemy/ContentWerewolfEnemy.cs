﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Alternative targeting priority system:
//If there is a building in range, chart a path to it that allows passing player units. 
//If there are no player units on the path, charge the building. If there are, attack the first unit on the path, and try to progress down the path.
public class ContentWerewolfEnemy : GameEnemyUnit
{
    public ContentWerewolfEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 60;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 12;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Werewolf";
        m_desc = "";

        m_minWave = 5;
        m_maxWave = 6;

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit));
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit));
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit));
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit));

        LateInit();

        //Needs to happen after LateInit because it does math based on maxHealth
        m_keywordHolder.m_keywords.Add(new GameRegenerateKeyword(m_maxHealth));

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_keywordHolder.m_keywords.Add(new GameMomentumKeyword(new GameGainPowerAction(this, 5)));
        }
    }
}