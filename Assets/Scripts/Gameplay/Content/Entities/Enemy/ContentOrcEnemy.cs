using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrcEnemy : GameEnemyEntity
{
    public ContentOrcEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 22;
        m_maxAP = 6;
        m_apRegen = 3;
        m_power = 6;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 3;
        m_maxWave = 4;

        m_name = "Orc";
        m_desc = "Feirce and strong, the backbone of any good army.";

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStandardStep(m_AIGameEnemyEntity));

        LateInit();
    }
}