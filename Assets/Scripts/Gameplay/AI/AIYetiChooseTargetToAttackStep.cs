using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIYetiChooseTargetToAttackStep : AIChooseTargetToAttackStandardStep
{
    public AIYetiChooseTargetToAttackStep(AIGameEnemyUnit AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override void TakeStep()
    {
        GameUnit closestVulnerableEntityInRage = FindClosestVulnerableUnitInRange();
        if (closestVulnerableEntityInRage != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestVulnerableEntityInRage;
        }

        GameBuildingBase closestVulnerableBuildingInRange = FindClosestVulnerableBuildingInRange();
        if (closestVulnerableBuildingInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestVulnerableBuildingInRange;
            return;
        }

        GameUnit closestEntityInRange = FindClosestEntityInRange();
        if (closestEntityInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestEntityInRange;
            return;
        }

        GameBuildingBase closestBuildingInRange = FindClosestBuildingInRange();
        if (closestBuildingInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestBuildingInRange;
            return;
        }

        m_AIGameEnemyUnit.m_targetGameElement = null;
    }
}