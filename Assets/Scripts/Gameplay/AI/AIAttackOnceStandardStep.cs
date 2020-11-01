using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackOnceStandardStep : AIStep
{
    public AIAttackOnceStandardStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep(bool shouldYield)
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
                    if (shouldYield)
                    {
                        UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                        while (UICameraController.Instance.IsCameraSmoothing())
                        {
                            yield return null;
                        }
                    }

                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitUnit(gameUnit, m_AIGameEnemyUnit.m_gameEnemyUnit.GetDamageToDealTo(gameUnit));

                    if (shouldYield)
                    {
                        //UIHelper.CreateWorldElementNotification("Does AI step: " + GetType(), true, m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                        yield return new WaitForSeconds(0.5f);
                    }

                    if (gameUnit == null || gameUnit.m_isDead)
                    {
                        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack())
                        {
                            m_AIGameEnemyUnit.m_doSteps = true;
                        }
                        yield break;
                    }
                    break;
                case GameBuildingBase gameBuilding:
                    if (shouldYield)
                    {
                        UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                        while (UICameraController.Instance.IsCameraSmoothing())
                        {
                            yield return null;
                        }
                    }

                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitBuilding(gameBuilding);

                    if (shouldYield)
                    {
                        //UIHelper.CreateWorldElementNotification("Does AI step: " + GetType(), true, m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                        yield return new WaitForSeconds(0.5f);
                    }

                    if (gameBuilding.m_isDestroyed)
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
}
