using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AISiegebreakerChooseTargetToAttackStep : AIChooseTargetToAttackStandardStep
{
    public AISiegebreakerChooseTargetToAttackStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep(bool yield)
    {
        GameBuildingBase castleInRange = FindCastleInRange();
        if (castleInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = castleInRange;
            yield break;
        }

        GameBuildingBase closestDefensiveBuildingInRange = FindClosestDefensiveBuildingInRange();
        if (closestDefensiveBuildingInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestDefensiveBuildingInRange;
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

    protected GameBuildingBase FindClosestDefensiveBuildingInRange()
    {
        if (m_AIGameEnemyUnit.m_possibleBuildingTargets.Count == 1)
        {
            if (m_AIGameEnemyUnit.m_possibleBuildingTargets[0].m_buildingType == BuildingType.Defensive)
            {
                return m_AIGameEnemyUnit.m_possibleBuildingTargets[0];
            }
            else
            {
                return null;
            }
        }
        else if (m_AIGameEnemyUnit.m_possibleBuildingTargets.Count > 1)
        {
            if (m_AIGameEnemyUnit.m_possibleBuildingTargets.Any(b => b.m_buildingType == BuildingType.Defensive))
            {
                int closestEnemyBuilding = m_AIGameEnemyUnit.m_possibleBuildingTargets.Where(b => b.m_buildingType == BuildingType.Defensive).Min(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), b.GetGameTile()));
                return m_AIGameEnemyUnit.m_possibleBuildingTargets.Where(b => b.m_buildingType == BuildingType.Defensive).First(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), b.GetGameTile()) == closestEnemyBuilding);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}