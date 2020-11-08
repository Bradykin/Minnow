using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AILavaRhinoMoveToTargetStep : AIMoveToTargetStandardStep
{
    public AILavaRhinoMoveToTargetStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep(bool shouldYield)
    {
        if (shouldYield)
        {
            yield return FactoryManager.Instance.StartCoroutine(MoveToTarget(shouldYield, m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina(), true));
        }
        else
        {
            FactoryManager.Instance.StartCoroutine(MoveToTarget(shouldYield, m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina(), true));
        }
    }
}