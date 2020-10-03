using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIChooseTargetToAttackStandardStep : AIStep
{
    public AIChooseTargetToAttackStandardStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

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

        GameBuildingBase castleInRange = FindCastleInRange();
        if (castleInRange != null && m_AIGameEnemyEntity.m_gameEnemyEntity.IsInRangeOfBuilding(castleInRange))
        {
            m_AIGameEnemyEntity.m_targetGameElement = castleInRange;
            yield break;
        }

        GameEntity closestEntityInRange = FindClosestEntityInRange();
        if (closestEntityInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = closestEntityInRange;
            yield break;
        }

        if (castleInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = castleInRange;
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
            int closestEnemyEntity = m_AIGameEnemyEntity.m_possibleEntityTargets.Min(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), e.GetGameTile()));
            return m_AIGameEnemyEntity.m_possibleEntityTargets.First(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), e.GetGameTile()) == closestEnemyEntity);
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
            int closestEnemyBuilding = m_AIGameEnemyEntity.m_possibleBuildingTargets.Min(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), b.GetGameTile()));
            return m_AIGameEnemyEntity.m_possibleBuildingTargets.First(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), b.GetGameTile()) == closestEnemyBuilding);
        }
        else
        {
            return null;
        }
    }

    protected GameEntity FindClosestVulnerableEntityInRange()
    {
        if (m_AIGameEnemyEntity.m_vulnerableEntityTargets.Count == 1)
        {
            return m_AIGameEnemyEntity.m_vulnerableEntityTargets[0];
        }
        else if (m_AIGameEnemyEntity.m_vulnerableEntityTargets.Count > 1)
        {
            int closestEnemyEntity = m_AIGameEnemyEntity.m_vulnerableEntityTargets.Min(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), e.GetGameTile()));
            return m_AIGameEnemyEntity.m_vulnerableEntityTargets.First(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), e.GetGameTile()) == closestEnemyEntity);
        }
        else
        {
            return null;
        }
    }

    protected GameBuildingBase FindClosestVulnerableBuildingInRange()
    {
        if (m_AIGameEnemyEntity.m_vulnerableBuildingTargets.Count == 1)
        {
            return m_AIGameEnemyEntity.m_vulnerableBuildingTargets[0];
        }
        else if (m_AIGameEnemyEntity.m_vulnerableBuildingTargets.Count > 1)
        {
            int closestEnemyBuilding = m_AIGameEnemyEntity.m_vulnerableBuildingTargets.Min(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), b.GetGameTile()));
            return m_AIGameEnemyEntity.m_vulnerableBuildingTargets.First(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), b.GetGameTile()) == closestEnemyBuilding);
        }
        else
        {
            return null;
        }
    }
}