using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWerewolfEnemy : GameEnemyEntity
{
    public ContentWerewolfEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 22;
        m_maxAP = 6;
        m_apRegen = 4;
        m_power = 9;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Werewolf";
        m_desc = "This thing never stops healing!";

        m_minWave = 6;

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStandardStep(m_AIGameEnemyEntity));

        LateInit();

        //Needs to happen after LateInit because it does math based on maxHealth
        m_keywordHolder.m_keywords.Add(new GameRegenerateKeyword(m_maxHealth));
    }
}