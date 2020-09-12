using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentYetiEnemy : GameEnemyEntity
{
    public ContentYetiEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 10;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 5;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Yeti";
        m_desc = "Is it... is it throwing snowballs?";

        m_minWave = 5;

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(3));

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStep(m_AIGameEnemyEntity));

        LateInit();
    }
}