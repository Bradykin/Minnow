using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentToadEnemy : GameEnemyEntity
{
    public ContentToadEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 6;
        m_maxAP = 6;
        m_apRegen = 2;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Toad";
        m_desc = "Don't let this thing hit you; it'll drain your AP!";

        m_minWave = 5;

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStep(m_AIGameEnemyEntity));

        LateInit();
    }

    public override int HitEntity(GameEntity other)
    {
        int damageTaken = base.HitEntity(other);

        other.EmptyAP();

        return damageTaken;
    }
}