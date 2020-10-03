using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AISiegebreakerMoveToTargetStep : AIMoveToTargetStandardStep
{
    public AISiegebreakerMoveToTargetStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep()
    {
        yield return FactoryManager.Instance.StartCoroutine(MoveToTarget(m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen(), true));
    }
}