using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Target buildings over units
public class ContentAngryBirdEnemy : GameEnemyEntity
{
    public ContentAngryBirdEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 4;
        m_maxAP = 8;
        m_apRegen = 5;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Angry Bird";
        m_desc = "";

        m_minWave = 3;
        m_maxWave = 4;

        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStandardStep(m_AIGameEnemyEntity));

        LateInit();
    }
}