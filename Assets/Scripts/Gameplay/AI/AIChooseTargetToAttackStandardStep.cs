using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIChooseTargetToAttackStandardStep : AIStep
{
    public AIChooseTargetToAttackStandardStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep(bool shouldYield)
    {
        GameUnit closestTauntUnitInRange = FindClosestTauntUnitInRange();
        if (closestTauntUnitInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestTauntUnitInRange;
            yield break;
        }

        GameUnit closestVulnerableUnitInRange = FindClosestVulnerableUnitInRange();
        if (closestVulnerableUnitInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestVulnerableUnitInRange;
            yield break;
        }

        GameBuildingBase castleInRange = FindCastleInRange();
        if (castleInRange != null && m_AIGameEnemyUnit.m_gameEnemyUnit.IsInRangeOfBuilding(castleInRange))
        {
            m_AIGameEnemyUnit.m_targetGameElement = castleInRange;
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

        if (castleInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = castleInRange;
            yield break;
        }

        GameBuildingBase closestBuildingInRange = FindClosestBuildingInRange();
        if (closestBuildingInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestBuildingInRange;
            yield break;
        }

        m_AIGameEnemyUnit.m_targetGameElement = null;


        GameTile moveDestination = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GameHelper.GetPlayer().GetCastleGameTile(), Mathf.Min(m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina(), m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen()));
        m_AIGameEnemyUnit.m_targetGameTile = moveDestination;
    }

    protected GameBuildingBase FindCastleInRange()
    {
        GameBuildingBase castleInRange = m_AIGameEnemyUnit.m_possibleBuildingTargets.FirstOrDefault(b => b is ContentCastleBuilding);

        return castleInRange;
    }

    protected GameUnit FindClosestUnitInRange()
    {
        if (m_AIGameEnemyUnit.m_possibleUnitTargets.Count == 1)
        {
            return m_AIGameEnemyUnit.m_possibleUnitTargets[0];
        }
        else if (m_AIGameEnemyUnit.m_possibleUnitTargets.Count > 1)
        {
            int closestEnemyUnit = m_AIGameEnemyUnit.m_possibleUnitTargets.Min(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), e.GetGameTile()));
            return m_AIGameEnemyUnit.m_possibleUnitTargets.First(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), e.GetGameTile()) == closestEnemyUnit);
        }
        else
        {
            return null;
        }
    }

    protected GameBuildingBase FindClosestBuildingInRange()
    {
        if (m_AIGameEnemyUnit.m_possibleBuildingTargets.Count == 1)
        {
            return m_AIGameEnemyUnit.m_possibleBuildingTargets[0];
        }
        else if (m_AIGameEnemyUnit.m_possibleBuildingTargets.Count > 1)
        {
            int closestEnemyBuilding = m_AIGameEnemyUnit.m_possibleBuildingTargets.Min(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), b.GetGameTile()));
            return m_AIGameEnemyUnit.m_possibleBuildingTargets.First(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), b.GetGameTile()) == closestEnemyBuilding);
        }
        else
        {
            return null;
        }
    }

    protected GameUnit FindClosestVulnerableUnitInRange()
    {
        if (m_AIGameEnemyUnit.m_vulnerableUnitTargets.Count == 1)
        {
            return m_AIGameEnemyUnit.m_vulnerableUnitTargets[0];
        }
        else if (m_AIGameEnemyUnit.m_vulnerableUnitTargets.Count > 1)
        {
            int closestEnemyUnit = m_AIGameEnemyUnit.m_vulnerableUnitTargets.Min(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), e.GetGameTile()));
            return m_AIGameEnemyUnit.m_vulnerableUnitTargets.First(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), e.GetGameTile()) == closestEnemyUnit);
        }
        else
        {
            return null;
        }
    }

    protected GameBuildingBase FindClosestVulnerableBuildingInRange()
    {
        if (m_AIGameEnemyUnit.m_vulnerableBuildingTargets.Count == 1)
        {
            return m_AIGameEnemyUnit.m_vulnerableBuildingTargets[0];
        }
        else if (m_AIGameEnemyUnit.m_vulnerableBuildingTargets.Count > 1)
        {
            int closestEnemyBuilding = m_AIGameEnemyUnit.m_vulnerableBuildingTargets.Min(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), b.GetGameTile()));
            return m_AIGameEnemyUnit.m_vulnerableBuildingTargets.First(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), b.GetGameTile()) == closestEnemyBuilding);
        }
        else
        {
            return null;
        }
    }

    protected GameUnit FindClosestTauntUnitInRange()
    {
        if (m_AIGameEnemyUnit.m_tauntUnitTargets.Count == 1)
        {
            return m_AIGameEnemyUnit.m_tauntUnitTargets[0];
        }
        else if (m_AIGameEnemyUnit.m_tauntUnitTargets.Count > 1)
        {
            int closestEnemyUnit = m_AIGameEnemyUnit.m_tauntUnitTargets.Min(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), e.GetGameTile()));
            return m_AIGameEnemyUnit.m_tauntUnitTargets.First(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), e.GetGameTile()) == closestEnemyUnit);
        }
        else
        {
            return null;
        }
    }
}