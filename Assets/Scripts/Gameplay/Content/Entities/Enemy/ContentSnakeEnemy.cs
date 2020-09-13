using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnakeEnemy : GameEnemyEntity
{
    public ContentSnakeEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 3;
        m_maxAP = 6;
        m_apRegen = 3;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Snake";
        m_desc = "It's venom permanently drains 2 power.";
        m_minWave = 4;

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStep(m_AIGameEnemyEntity));

        LateInit();
    }

    public override int HitEntity(GameEntity other, bool spendAP = true)
    {
        int damageTaken = base.HitEntity(other, spendAP);

        other.AddPower(-2);

        return damageTaken;
    }
}