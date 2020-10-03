using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISiegebreakerAttackUntilOutOfStaminaStep : AIAttackUntilOutOfStaminaStandardStep
{
    public AISiegebreakerAttackUntilOutOfStaminaStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep()
    {
        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina() == m_AIGameEnemyUnit.m_gameEnemyUnit.GetMaxStamina())
        {
            yield return FactoryManager.Instance.StartCoroutine(base.TakeStep());
        }
    }
}
