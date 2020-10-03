﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Alternative targeting priority system:
//If there is a building in range, chart a path to it that allows passing player units. 
//If there are no player units on the path, charge the building. If there are, attack the first unit on the path, and try to progress down the path.
public class ContentWerewolfEnemy : GameEnemyEntity
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

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyEntity));

        LateInit();

        //Needs to happen after LateInit because it does math based on maxHealth
        m_keywordHolder.m_keywords.Add(new GameRegenerateKeyword(m_maxHealth));
    }
}