using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For AI:
//Favours staying in or near water
//Does a move-attack-move
public class ContentLizardmanEnemy : GameEnemyUnit
{
    public ContentLizardmanEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 15;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 12;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Lizardman";
        m_desc = "Swims instantly through all water tiles.";

        m_minWave = 5;
        m_maxWave = 6;

        m_keywordHolder.m_keywords.Add(new GameWaterwalkKeyword());
        m_keywordHolder.m_keywords.Add(new GameDamageShieldKeyword(2));

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AILizardmanChooseTargetToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AILizardmanFleeToWaterStep(m_AIGameEnemyEntity));

        LateInit();
    }
}