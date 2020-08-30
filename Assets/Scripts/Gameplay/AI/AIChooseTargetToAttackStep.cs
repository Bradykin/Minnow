using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIChooseTargetToAttackStep : AIStep
{
    public AIChooseTargetToAttackStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override void TakeStep()
    {
        GameBuildingBase castleInRange = m_AIGameEnemyEntity.m_possibleBuildingTargets.FirstOrDefault(b => b is ContentCastleBuilding);
        if (castleInRange != null)
        {
            m_AIGameEnemyEntity.m_targetToAttack = castleInRange;
            return;
        }

        if (m_AIGameEnemyEntity.m_possibleEntityTargets.Count == 1)
        {
            m_AIGameEnemyEntity.m_targetToAttack = m_AIGameEnemyEntity.m_possibleEntityTargets[0];
            return;
        }
        else if (m_AIGameEnemyEntity.m_possibleEntityTargets.Count > 1)
        {
            int closestEnemyEntity = m_AIGameEnemyEntity.m_possibleEntityTargets.Min(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, e.m_curTile));
            m_AIGameEnemyEntity.m_targetToAttack = m_AIGameEnemyEntity.m_possibleEntityTargets.First(e => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, e.m_curTile) == closestEnemyEntity);
        }

        if (m_AIGameEnemyEntity.m_possibleBuildingTargets.Count == 1)
        {
            m_AIGameEnemyEntity.m_targetToAttack = m_AIGameEnemyEntity.m_possibleBuildingTargets[0];
            return;
        }
        else if (m_AIGameEnemyEntity.m_possibleBuildingTargets.Count > 1)
        {
            int closestEnemyBuilding = m_AIGameEnemyEntity.m_possibleBuildingTargets.Min(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, b.m_curTile.GetGameTile()));
            m_AIGameEnemyEntity.m_targetToAttack = m_AIGameEnemyEntity.m_possibleBuildingTargets.First(b => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, b.m_curTile.GetGameTile()) == closestEnemyBuilding);
        }

        m_AIGameEnemyEntity.m_targetToAttack = null;
    }
}