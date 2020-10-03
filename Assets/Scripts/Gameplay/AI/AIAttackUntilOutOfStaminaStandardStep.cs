using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackUntilOutOfStaminaStandardStep : AIStep
{
    public AIAttackUntilOutOfStaminaStandardStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override IEnumerator TakeStep()
    {
        if (m_AIGameEnemyEntity.m_targetGameElement == null || !m_AIGameEnemyEntity.m_gameEnemyEntity.IsInRangeOfGameElement(m_AIGameEnemyEntity.m_targetGameElement))
        {
            yield break;
        }

        bool useSteppedOutTurn = m_AIGameEnemyEntity.UseSteppedOutTurn;

        while(m_AIGameEnemyEntity.m_gameEnemyEntity.HasStaminaToAttack())
        {
            bool didAttack = false;
            switch (m_AIGameEnemyEntity.m_targetGameElement)
            {
                case GameEntity gameEntity:
                    if (useSteppedOutTurn)
                    {
                        UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyEntity.m_gameEnemyEntity.GetWorldTile().gameObject);
                        while (UICameraController.Instance.IsCameraSmoothing())
                        {
                            yield return null;
                        }
                    }

                    didAttack = true;
                    m_AIGameEnemyEntity.m_gameEnemyEntity.HitEntity(gameEntity);

                    if (useSteppedOutTurn)
                    {
                        UIHelper.CreateWorldElementNotification("Does AI step: " + GetType(), true, m_AIGameEnemyEntity.m_gameEnemyEntity.GetWorldTile().gameObject);
                        yield return new WaitForSeconds(0.5f);
                    }

                    if (gameEntity.m_isDead || gameEntity == null)
                    {
                        if (m_AIGameEnemyEntity.m_gameEnemyEntity.HasStaminaToAttack())
                        {
                            m_AIGameEnemyEntity.m_doSteps = true;
                        }
                        yield break;
                    }
                    break;
                case GameBuildingBase gameBuilding:
                    if (useSteppedOutTurn)
                    {
                        UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyEntity.m_gameEnemyEntity.GetWorldTile().gameObject);
                        while (UICameraController.Instance.IsCameraSmoothing())
                        {
                            yield return null;
                        }
                    }

                    didAttack = true;
                    m_AIGameEnemyEntity.m_gameEnemyEntity.HitBuilding(gameBuilding);

                    if (useSteppedOutTurn)
                    {
                        UIHelper.CreateWorldElementNotification("Does AI step: " + GetType(), true, m_AIGameEnemyEntity.m_gameEnemyEntity.GetWorldTile().gameObject);
                        yield return new WaitForSeconds(0.5f);
                    }

                    if (gameBuilding.m_isDestroyed)
                    {
                        if (m_AIGameEnemyEntity.m_gameEnemyEntity.HasStaminaToAttack())
                        {
                            m_AIGameEnemyEntity.m_doSteps = true;
                        }
                        yield break;
                    }
                    break;
            }

            if (!didAttack)
                yield break;
        }
    }
}
