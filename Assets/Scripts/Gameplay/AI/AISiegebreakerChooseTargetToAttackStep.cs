using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AISiegebreakerChooseTargetToAttackStep : AIChooseTargetToAttackStandardStep
{
    public AISiegebreakerChooseTargetToAttackStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override IEnumerator TakeStep()
    {
        GameBuildingBase castleInRange = FindCastleInRange();
        if (castleInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = castleInRange;
            yield break;
        }

        GameBuildingBase closestDefensiveBuildingInRange = FindClosestDefensiveBuildingInRange();
        if (closestDefensiveBuildingInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = closestDefensiveBuildingInRange;
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

    protected GameBuildingBase FindClosestDefensiveBuildingInRange()
    {
        if (m_AIGameEnemyEntity.m_possibleBuildingTargets.Count == 1)
        {
            if (m_AIGameEnemyEntity.m_possibleBuildingTargets[0].m_buildingType == BuildingType.Defensive)
            {
                return m_AIGameEnemyEntity.m_possibleBuildingTargets[0];
            }
            else
            {
                return null;
            }
        }
        else if (m_AIGameEnemyEntity.m_possibleBuildingTargets.Count > 1)
        {
            if (m_AIGameEnemyEntity.m_possibleBuildingTargets.Any(b => b.m_buildingType == BuildingType.Defensive))
            {
                int closestEnemyBuilding = m_AIGameEnemyEntity.m_possibleBuildingTargets.Where(b => b.m_buildingType == BuildingType.Defensive).Min(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), b.GetGameTile()));
                return m_AIGameEnemyEntity.m_possibleBuildingTargets.Where(b => b.m_buildingType == BuildingType.Defensive).First(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), b.GetGameTile()) == closestEnemyBuilding);
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