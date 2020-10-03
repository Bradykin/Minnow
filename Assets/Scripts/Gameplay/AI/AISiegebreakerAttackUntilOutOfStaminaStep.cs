using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISiegebreakerAttackUntilOutOfStaminaStep : AIAttackUntilOutOfStaminaStandardStep
{
    public AISiegebreakerAttackUntilOutOfStaminaStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override IEnumerator TakeStep()
    {
        if (m_AIGameEnemyEntity.m_gameEnemyEntity.GetCurStamina() == m_AIGameEnemyEntity.m_gameEnemyEntity.GetMaxStamina())
        {
            yield return FactoryManager.Instance.StartCoroutine(base.TakeStep());
        }
    }
}
