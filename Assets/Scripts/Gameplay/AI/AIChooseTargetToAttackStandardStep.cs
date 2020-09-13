using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIChooseTargetToAttackStandardStep : AIStep
{
    public AIChooseTargetToAttackStandardStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override void TakeStep()
    {
        GameBuildingBase castleInRange = FindCastleInRange();
        if (castleInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = castleInRange;
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

    protected GameBuildingBase FindCastleInRange()
    {
        GameBuildingBase castleInRange = m_AIGameEnemyEntity.m_possibleBuildingTargets.FirstOrDefault(b => b is ContentCastleBuilding);

        return castleInRange;
    }

    protected GameEntity FindClosestEntityInRange()
    {
        if (m_AIGameEnemyEntity.m_possibleEntityTargets.Count == 1)
        {
            return m_AIGameEnemyEntity.m_possibleEntityTargets[0];
        }
        else if (m_AIGameEnemyEntity.m_possibleEntityTargets.Count > 1)
        {
            int closestEnemyEntity = m_AIGameEnemyEntity.m_possibleEntityTargets.Min(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, e.m_curTile));
            return m_AIGameEnemyEntity.m_possibleEntityTargets.First(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, e.m_curTile) == closestEnemyEntity);
        }
        else
        {
            return null;
        }
    }

    protected GameBuildingBase FindClosestBuildingInRange()
    {
        if (m_AIGameEnemyEntity.m_possibleBuildingTargets.Count == 1)
        {
            return m_AIGameEnemyEntity.m_possibleBuildingTargets[0];
        }
        else if (m_AIGameEnemyEntity.m_possibleBuildingTargets.Count > 1)
        {
            int closestEnemyBuilding = m_AIGameEnemyEntity.m_possibleBuildingTargets.Min(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, b.m_curTile.GetGameTile()));
            return m_AIGameEnemyEntity.m_possibleBuildingTargets.First(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, b.m_curTile.GetGameTile()) == closestEnemyBuilding);
        }
        else
        {
            return null;
        }
    }
}