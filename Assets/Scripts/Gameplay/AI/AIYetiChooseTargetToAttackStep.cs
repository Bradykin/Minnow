using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIYetiChooseTargetToAttackStep : AIChooseTargetToAttackStandardStep
{
    public AIYetiChooseTargetToAttackStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep(bool shouldYield)
    {
        GameUnit closestVulnerableUnitInRange = FindClosestVulnerableUnitInRange();
        if (closestVulnerableUnitInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestVulnerableUnitInRange;
            yield break;
        }

        GameBuildingBase closestVulnerableBuildingInRange = FindClosestVulnerableBuildingInRange();
        if (closestVulnerableBuildingInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestVulnerableBuildingInRange;
            yield break;
        }

        GameUnit closestUnitInRange = FindClosestUnitInRange();
        if (closestUnitInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestUnitInRange;
            yield break;
        }

        GameBuildingBase closestBuildingInRange = FindClosestBuildingInRange();
        if (closestBuildingInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestBuildingInRange;
            yield break;
        }

        m_AIGameEnemyUnit.m_targetGameElement = null;

        GameTile moveDestination = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GameHelper.GetPlayer().Castle.GetGameTile(), Mathf.Min(m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina(), m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen()));
        m_AIGameEnemyUnit.m_targetGameTile = moveDestination;
    }
}