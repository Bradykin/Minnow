using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AISiegebreakerMoveToTargetStep : AIMoveToTargetStandardStep
{
    public AISiegebreakerMoveToTargetStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override void TakeStep()
    {
        MoveToTarget(m_AIGameEnemyEntity.m_gameEnemyEntity.GetCurStamina(), true);
    }
}