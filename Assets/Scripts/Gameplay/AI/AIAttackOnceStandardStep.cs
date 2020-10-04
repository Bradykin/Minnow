using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackOnceStandardStep : AIStep
{
    public AIAttackOnceStandardStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep(bool yield)
    {
        if (m_AIGameEnemyUnit.m_targetGameElement == null || !m_AIGameEnemyUnit.m_gameEnemyUnit.IsInRangeOfGameElement(m_AIGameEnemyUnit.m_targetGameElement))
        {
            yield break;
        }

        bool useSteppedOutTurn = yield && m_AIGameEnemyUnit.UseSteppedOutTurn;

        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack())
        {
            switch (m_AIGameEnemyUnit.m_targetGameElement)
            {
                case GameUnit gameUnit:
                    if (useSteppedOutTurn)
                    {
                        UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                        while (UICameraController.Instance.IsCameraSmoothing())
                        {
                            yield return null;
                        }
                    }

                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitUnit(gameUnit);

                    if (useSteppedOutTurn)
                    {
                        //UIHelper.CreateWorldElementNotification("Does AI step: " + GetType(), true, m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                        yield return new WaitForSeconds(0.5f);
                    }

                    if (gameUnit.m_isDead || gameUnit == null)
                    {
                        if (m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack())
                        {
                            m_AIGameEnemyUnit.m_doSteps = true;
                        }
                        yield break;
                    }
                    break;
                case GameBuildingBase gameBuilding:
                    if (useSteppedOutTurn)
                    {
                        UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                        while (UICameraController.Instance.IsCameraSmoothing())
                        {
                            yield return null;
                        }
                    }

                    m_AIGameEnemyUnit.m_gameEnemyUnit.HitBuilding(gameBuilding);

                    if (useSteppedOutTurn)
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
