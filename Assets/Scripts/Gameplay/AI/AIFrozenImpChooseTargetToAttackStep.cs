using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIFrozenImpChooseTargetToAttackStep : AIChooseTargetToAttackStandardStep
{
    public AIFrozenImpChooseTargetToAttackStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep(bool shouldYield)
    {
        if (!m_AIGameEnemyUnit.m_gameEnemyUnit.m_isBoss)
        {
            GameUnit closestTauntUnitInRange = FindClosestTauntUnitInRange();
            if (closestTauntUnitInRange != null)
            {
                m_AIGameEnemyUnit.m_targetGameElement = closestTauntUnitInRange;
                yield break;
            }
        }

        GameUnit closestNotRootedUnitInRange = FindClosestNotRootedUnitInRange();
        if (closestNotRootedUnitInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestNotRootedUnitInRange;
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

    protected GameUnit FindClosestNotRootedUnitInRange()
    {
        List<GameUnit> notRootedTargets = new List<GameUnit>();
        for (int i = 0; i < m_AIGameEnemyUnit.m_possibleUnitTargets.Count; i++)
        {
            if (m_AIGameEnemyUnit.m_possibleUnitTargets[i].GetRootedKeyword() == null)
            {
                notRootedTargets.Add(m_AIGameEnemyUnit.m_possibleUnitTargets[i]);
            }
        }

        if (notRootedTargets.Count == 1)
        {
            return notRootedTargets[0];
        }
        else if (notRootedTargets.Count > 1)
        {
            int closestEnemyUnit = notRootedTargets.Min(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), e.GetGameTile()));
            return notRootedTargets.First(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), e.GetGameTile()) == closestEnemyUnit);
        }
        else
        {
            return null;
        }
    }
}