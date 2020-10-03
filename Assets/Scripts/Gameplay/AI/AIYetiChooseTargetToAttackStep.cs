using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIYetiChooseTargetToAttackStep : AIChooseTargetToAttackStandardStep
{
    public AIYetiChooseTargetToAttackStep(AIGameEnemyUnit AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override IEnumerator TakeStep()
    {
        GameUnit closestVulnerableEntityInRage = FindClosestVulnerableUnitInRange();
        if (closestVulnerableEntityInRage != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestVulnerableEntityInRage;
            yield break;
        }

        GameBuildingBase closestVulnerableBuildingInRange = FindClosestVulnerableBuildingInRange();
        if (closestVulnerableBuildingInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestVulnerableBuildingInRange;
            yield break;
        }

        GameUnit closestEntityInRange = FindClosestUnitInRange();
        if (closestEntityInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestEntityInRange;
            yield break;
        }

        GameBuildingBase closestBuildingInRange = FindClosestBuildingInRange();
        if (closestBuildingInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestBuildingInRange;
            yield break;
        }

        m_AIGameEnemyUnit.m_targetGameElement = null;
    }
}