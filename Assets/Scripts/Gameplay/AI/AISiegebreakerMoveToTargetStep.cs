using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AISiegebreakerMoveToTargetStep : AIMoveToTargetStandardStep
{
    public AISiegebreakerMoveToTargetStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep(bool yield)
    {
        if (yield)
        {
            yield return FactoryManager.Instance.StartCoroutine(MoveToTarget(yield, m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen(), true));
        }
        else
        {
            FactoryManager.Instance.StartCoroutine(MoveToTarget(yield, m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen(), true));
        }
    }
}