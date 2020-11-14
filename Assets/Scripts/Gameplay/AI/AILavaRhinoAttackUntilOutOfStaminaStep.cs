using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILavaRhinoAttackUntilOutOfStaminaStep : AIAttackUntilOutOfStaminaStandardStep
{
    public AILavaRhinoAttackUntilOutOfStaminaStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStepCoroutine()
    {
        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina() < m_AIGameEnemyUnit.m_gameEnemyUnit.GetMaxStamina())
        {
            UIHelper.CreateWorldElementNotification($"{m_AIGameEnemyUnit.m_gameEnemyUnit.GetName()} is charging up to attack!", false, m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
            yield break;
        }

        yield return FactoryManager.Instance.StartCoroutine(base.TakeStepCoroutine());
    }

    public override void TakeStepInstant()
    {
        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina() < m_AIGameEnemyUnit.m_gameEnemyUnit.GetMaxStamina())
        {
            UIHelper.CreateWorldElementNotification($"{m_AIGameEnemyUnit.m_gameEnemyUnit.GetName()} is charging up to attack!", false, m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
            return;
        }

        base.TakeStepInstant();
    }
}
