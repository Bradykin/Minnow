using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Refuse to stay out of fog of war????
//If no fog of war near player targets that are closish to them, head to the mountains?
public class ContentYetiEnemy : GameEnemyEntity
{
    public ContentYetiEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 110;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 8;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Yeti";
        m_desc = "Is it... is it throwing snowballs?";

        m_minWave = 4;
        m_maxWave = 6;

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(5));
        m_keywordHolder.m_keywords.Add(new GameMountainwalkKeyword());

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStandardStep(m_AIGameEnemyEntity));

        LateInit();
    }
}