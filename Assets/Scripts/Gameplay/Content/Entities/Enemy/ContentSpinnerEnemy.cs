using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For AI:
//Charges at a spot
//Attacks only once per turn, but hits all tiles around it
public class ContentSpinnerEnemy : GameEnemyEntity
{
    public ContentSpinnerEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 7;
        m_maxAP = 6;
        m_apRegen = 4;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 2;
        m_maxWave = 4;

        m_name = "Spinner";
        m_desc = "Why is he called a spinner?  He doesn't spin?";

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStandardStep(m_AIGameEnemyEntity));

        LateInit();
    }
}