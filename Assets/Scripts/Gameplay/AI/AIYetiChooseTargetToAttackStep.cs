using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIYetiChooseTargetToAttackStep : AIChooseTargetToAttackStandardStep
{
    public AIYetiChooseTargetToAttackStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override IEnumerator TakeStep()
    {
        GameEntity closestVulnerableEntityInRage = FindClosestVulnerableEntityInRange();
        if (closestVulnerableEntityInRage != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = closestVulnerableEntityInRage;
            yield break;
        }

        GameBuildingBase closestVulnerableBuildingInRange = FindClosestVulnerableBuildingInRange();
        if (closestVulnerableBuildingInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = closestVulnerableBuildingInRange;
            yield break;
        }

        GameEntity closestEntityInRange = FindClosestEntityInRange();
        if (closestEntityInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = closestEntityInRange;
            yield break;
        }

        GameBuildingBase closestBuildingInRange = FindClosestBuildingInRange();
        if (closestBuildingInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = closestBuildingInRange;
            yield break;
        }

        m_AIGameEnemyEntity.m_targetGameElement = null;
    }
}