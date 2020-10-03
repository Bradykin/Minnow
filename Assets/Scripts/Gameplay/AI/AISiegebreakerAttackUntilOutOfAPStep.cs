using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISiegebreakerAttackUntilOutOfAPStep : AIAttackUntilOutOfAPStandardStep
{
    public AISiegebreakerAttackUntilOutOfAPStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override IEnumerator TakeStep()
    {
        if (m_AIGameEnemyEntity.m_gameEnemyEntity.GetCurAP() == m_AIGameEnemyEntity.m_gameEnemyEntity.GetMaxAP())
        {
            yield return FactoryManager.Instance.StartCoroutine(base.TakeStep());
        }
    }
}
