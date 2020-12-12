using Game.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIZombieShipSeekLandStep : AIMoveStep
{
    private ContentZombieShipEnemy zombieShipEnemy;

    public AIZombieShipSeekLandStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) 
    {
        if (!(m_AIGameEnemyUnit.m_gameEnemyUnit is ContentZombieShipEnemy))
        {
            Debug.LogError("Wrong unit using Zombie Ship AI script.");
        }

        zombieShipEnemy = (ContentZombieShipEnemy)m_AIGameEnemyUnit.m_gameEnemyUnit;
    }

    public override IEnumerator TakeStepCoroutine()
    {
        if (zombieShipEnemy.m_hasReleasedUnits)
        {
            yield break;
        }

        yield return FactoryManager.Instance.StartCoroutine(MoveTowardsCastleCoroutine());
        yield return new WaitForSeconds(0.5f);
    }

    public override void TakeStepInstant()
    {
        if (zombieShipEnemy.m_hasReleasedUnits)
        {
            return;
        }

        MoveTowardsCastleInstant();
    }
}
