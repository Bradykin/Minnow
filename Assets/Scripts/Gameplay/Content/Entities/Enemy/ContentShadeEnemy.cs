using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadeEnemy : GameEnemyUnit
{
    public ContentShadeEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 12;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 7;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Shade";
        m_desc = "";

        m_minWave = 4;
        m_maxWave = 4;

        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());
        m_keywordHolder.m_keywords.Add(new GameDamageShieldKeyword(2));

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyEntity));

        LateInit();
    }
}