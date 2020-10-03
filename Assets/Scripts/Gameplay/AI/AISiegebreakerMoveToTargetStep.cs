using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AISiegebreakerMoveToTargetStep : AIMoveToTargetStandardStep
{
    public AISiegebreakerMoveToTargetStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override IEnumerator TakeStep()
    {
        yield return FactoryManager.Instance.StartCoroutine(MoveToTarget(m_AIGameEnemyEntity.m_gameEnemyEntity.GetCurAP(), true));
    }
}