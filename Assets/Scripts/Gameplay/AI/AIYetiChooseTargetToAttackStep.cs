using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIYetiChooseTargetToAttackStep : AIChooseTargetToAttackStandardStep
{
    public AIYetiChooseTargetToAttackStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override void TakeStep()
    {
        GameEntity closestVulnerableEntityInRage = FindClosestVulnerableEntityInRange();
        if (closestVulnerableEntityInRage != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = closestVulnerableEntityInRage;
        }

        GameBuildingBase closestVulnerableBuildingInRange = FindClosestVulnerableBuildingInRange();
        if (closestVulnerableBuildingInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = closestVulnerableBuildingInRange;
            return;
        }

        GameEntity closestEntityInRange = FindClosestEntityInRange();
        if (closestEntityInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = closestEntityInRange;
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