using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLizardmanEnemy : GameEnemyEntity
{
    public ContentLizardmanEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 6;
        m_maxAP = 5;
        m_apRegen = 3;
        m_power = 5;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Lizardman";
        m_desc = "It walks on land, it swims in water.  Is there anything it can't do?";

        m_minWave = 5;

        m_keywordHolder.m_keywords.Add(new GameWaterwalkKeyword());

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStep(m_AIGameEnemyEntity));

        LateInit();
    }
}