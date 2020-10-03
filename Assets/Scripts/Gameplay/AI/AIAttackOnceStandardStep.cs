using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackOnceStandardStep : AIStep
{
    public AIAttackOnceStandardStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override void TakeStep()
    {
        if (m_AIGameEnemyUnit.m_targetGameElement == null || !m_AIGameEnemyUnit.m_gameEnemyUnit.IsInRangeOfGameElement(m_AIGameEnemyUnit.m_targetGameElement))
        {
            return;
        }

        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack())
        {
            switch (m_AIGameEnemyUnit.m_targetGameElement)
            {
                case GameUnit gameUnit:
                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitUnit(gameUnit);
                    if (gameUnit.m_isDead || gameUnit == null)
                    {
                        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack())
                        {
                            m_AIGameEnemyUnit.m_doSteps = true;
                        }
                        return;
                    }
                    break;
                case GameBuildingBase gameBuildingBase:
                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitBuilding(gameBuildingBase);
                    if (gameBuildingBase.m_isDestroyed)
                    {
                        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack())
                        {
                            m_AIGameEnemyUnit.m_doSteps = true;
                        }
                        return;
                    }
                    break;
            }
        }
    }
}
