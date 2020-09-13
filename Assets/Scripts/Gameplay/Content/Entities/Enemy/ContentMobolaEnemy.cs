using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMobolaEnemy : GameEnemyEntity
{
    public ContentMobolaEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 40;
        m_maxAP = 8;
        m_apRegen = 2;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Mobola";
        m_desc = "It only grows in power as it fights.";

        m_minWave = 6;

        m_keywordHolder.m_keywords.Add(new GameEnrageKeyword(new GameGainPowerAction(this, 3)));

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStandardStep(m_AIGameEnemyEntity));

        LateInit();
    }
}