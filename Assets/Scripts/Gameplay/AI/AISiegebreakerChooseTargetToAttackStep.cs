using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AISiegebreakerChooseTargetToAttackStep : AIChooseTargetToAttackStep
{
    public AISiegebreakerChooseTargetToAttackStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override void TakeStep()
    {
        GameBuildingBase castleInRange = FindCastleInRange();
        if (castleInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = castleInRange;
            return;
        }

        GameBuildingBase closestBuildingInRange = FindClosestBuildingInRange();
        if (closestBuildingInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = closestBuildingInRange;
            return;
        }

        m_AIGameEnemyEntity.m_targetGameElement = null;
    }
}