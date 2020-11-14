using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIFrozenImpChooseTargetToAttackStep : AIChooseTargetToAttackStandardStep
{
    public AIFrozenImpChooseTargetToAttackStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override void TakeStepInstant()
    {
        if (!m_AIGameEnemyUnit.m_gameEnemyUnit.m_isBoss)
        {
            GameUnit closestTauntUnitInRange = FindClosestTauntUnitInRange();
            if (closestTauntUnitInRange != null)
            {
                m_AIGameEnemyUnit.m_targetGameElement = closestTauntUnitInRange;
                return;
            }
        }

        GameUnit closestNotRootedUnitInRange = FindClosestNotRootedUnitInRange();
        if (closestNotRootedUnitInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestNotRootedUnitInRange;
            return;
        }

        base.TakeStepInstant();
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