using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AILavaRhinoChooseTargetToAttackStep : AIChooseTargetToAttackStandardStep
{
    public AILavaRhinoChooseTargetToAttackStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override void TakeStepInstant()
    {
        GameBuildingBase castleInRange = FindCastleInRange();
        if (castleInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = castleInRange;
            return;
        }

        GameBuildingBase closestDefensiveBuildingInRange = FindClosestDefensiveBuildingInRange();
        if (closestDefensiveBuildingInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestDefensiveBuildingInRange;
            return;
        }

        GameBuildingBase closestBuildingInRange = FindClosestBuildingInRange();
        if (closestBuildingInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestBuildingInRange;
            return;
        }

        m_AIGameEnemyUnit.m_targetGameElement = null;


        GameTile moveDestination = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GameHelper.GetPlayer().GetCastleGameTile(), m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina());
        m_AIGameEnemyUnit.m_targetGameTile = moveDestination;
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