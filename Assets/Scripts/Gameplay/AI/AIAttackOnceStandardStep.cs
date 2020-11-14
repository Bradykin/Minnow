using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackOnceStandardStep : AIStep
{
    protected bool repeatAI;
    
    public AIAttackOnceStandardStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStepCoroutine()
    {
        if (m_AIGameEnemyUnit.m_targetGameElement == null || !m_AIGameEnemyUnit.m_gameEnemyUnit.IsInRangeOfGameElement(m_AIGameEnemyUnit.m_targetGameElement))
        {
            yield break;
        }

        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack())
        {
            switch (m_AIGameEnemyUnit.m_targetGameElement)
            {
                case GameUnit gameUnit:
                    UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                    while (UICameraController.Instance.IsCameraSmoothing())
                    {
                        yield return null;
                    }

                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitUnit(gameUnit, m_AIGameEnemyUnit.m_gameEnemyUnit.GetDamageToDealTo(gameUnit));

                    yield return new WaitForSeconds(0.5f);

                    if (m_AIGameEnemyUnit.m_gameEnemyUnit.m_isDead)
                    {
                        yield break;
                    }

                    if (repeatAI)
                    {
                        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack())
                        {
                            m_AIGameEnemyUnit.m_doSteps = true;
                        }
                        yield break;
                    }
                    break;
                case GameBuildingBase gameBuilding:
                    UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                    while (UICameraController.Instance.IsCameraSmoothing())
                    {
                        yield return null;
                    }

                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitBuilding(gameBuilding);

                    yield return new WaitForSeconds(0.5f);

                    if (m_AIGameEnemyUnit.m_gameEnemyUnit.m_isDead)
                    {
                        yield break;
                    }

                    if (repeatAI)
                    {
                        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack())
                        {
                            m_AIGameEnemyUnit.m_doSteps = true;
                        }
                        yield break;
                    }
                    break;
            }
        }
    }

    public override void TakeStepInstant()
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
                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitUnit(gameUnit, m_AIGameEnemyUnit.m_gameEnemyUnit.GetDamageToDealTo(gameUnit));

                    if (m_AIGameEnemyUnit.m_gameEnemyUnit.m_isDead)
                    {
                        return;
                    }

                    if (repeatAI)
                    {
                        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack())
                        {
                            m_AIGameEnemyUnit.m_doSteps = true;
                        }
                        return;
                    }
                    break;
                case GameBuildingBase gameBuilding:
                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitBuilding(gameBuilding);

                    if (m_AIGameEnemyUnit.m_gameEnemyUnit.m_isDead)
                    {
                        return;
                    }

                    if (repeatAI)
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
