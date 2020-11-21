using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackUntilOutOfStaminaStandardStep : AIStep
{
    public AIAttackUntilOutOfStaminaStandardStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStepCoroutine()
    {
        if (m_AIGameEnemyUnit.m_targetGameElement == null || !m_AIGameEnemyUnit.m_gameEnemyUnit.IsInRangeOfGameElement(m_AIGameEnemyUnit.m_targetGameElement))
        {
            yield break;
        }

        while(m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack(m_AIGameEnemyUnit.m_targetGameElement))
        {
            bool didAttack = false;
            switch (m_AIGameEnemyUnit.m_targetGameElement)
            {
                case GameUnit gameUnit:
                    UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                    while (UICameraController.Instance.IsCameraSmoothing())
                    {
                        yield return null;
                    }

                    didAttack = true;
                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitUnit(gameUnit, m_AIGameEnemyUnit.m_gameEnemyUnit.GetDamageToDealTo(gameUnit));

                    yield return new WaitForSeconds(0.5f);

                    if (m_AIGameEnemyUnit.m_gameEnemyUnit.m_isDead)
                    {
                        yield break;
                    }

                    if (gameUnit == null || gameUnit.m_isDead || !m_AIGameEnemyUnit.m_gameEnemyUnit.IsInRangeOfGameElement(m_AIGameEnemyUnit.m_targetGameElement))
                    {
                        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack(null))
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

                    didAttack = true;
                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitBuilding(gameBuilding);

                    yield return new WaitForSeconds(0.5f);

                    if (m_AIGameEnemyUnit.m_gameEnemyUnit.m_isDead)
                    {
                        yield break;
                    }

                    if (gameBuilding.m_isDestroyed)
                    {
                        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack(null))
                        {
                            m_AIGameEnemyUnit.m_doSteps = true;
                        }
                        yield break;
                    }
                    break;
            }

            if (!didAttack)
            {
                yield break;
            }
        }
    }

    public override void TakeStepInstant()
    {
        if (m_AIGameEnemyUnit.m_targetGameElement == null || !m_AIGameEnemyUnit.m_gameEnemyUnit.IsInRangeOfGameElement(m_AIGameEnemyUnit.m_targetGameElement))
        {
            return;
        }

        while (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack(m_AIGameEnemyUnit.m_targetGameElement))
        {
            bool didAttack = false;
            switch (m_AIGameEnemyUnit.m_targetGameElement)
            {
                case GameUnit gameUnit:
                    didAttack = true;
                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitUnit(gameUnit, m_AIGameEnemyUnit.m_gameEnemyUnit.GetDamageToDealTo(gameUnit));

                    if (m_AIGameEnemyUnit.m_gameEnemyUnit.m_isDead)
                    {
                        return;
                    }

                    if (gameUnit == null || gameUnit.m_isDead || !m_AIGameEnemyUnit.m_gameEnemyUnit.IsInRangeOfGameElement(m_AIGameEnemyUnit.m_targetGameElement))
                    {
                        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack(null))
                        {
                            m_AIGameEnemyUnit.m_doSteps = true;
                        }
                        return;
                    }
                    break;
                case GameBuildingBase gameBuilding:
                    didAttack = true;
                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitBuilding(gameBuilding);

                    if (m_AIGameEnemyUnit.m_gameEnemyUnit.m_isDead)
                    {
                        return;
                    }

                    if (gameBuilding.m_isDestroyed)
                    {
                        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack(null))
                        {
                            m_AIGameEnemyUnit.m_doSteps = true;
                        }
                        return;
                    }
                    break;
            }

            if (!didAttack)
            {
                return;
            }
        }
    }
}
