using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AIMoveStep : AIStep
{
    public AIMoveStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    protected IEnumerator MoveTowardsCastle(bool shouldYield, int amountStaminaToSpend)
    {
        if (GameHelper.GetPlayer() != null && GameHelper.GetPlayer().Castle != null)
        {
            GameTile moveDestination;
            if (m_AIGameEnemyUnit.m_targetGameTile == null)
            {
                moveDestination = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GameHelper.GetPlayer().Castle.GetGameTile(), amountStaminaToSpend);
                m_AIGameEnemyUnit.m_targetGameTile = moveDestination;
            }
            else
            {
                moveDestination = m_AIGameEnemyUnit.m_targetGameTile;
            }

            if (moveDestination != m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile())
            {
                if (shouldYield)
                {
                    UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                    while (UICameraController.Instance.IsCameraSmoothing())
                    {
                        yield return null;
                    }
                }

                int moveDistance = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), moveDestination, true, false, true);
                m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination);

                if (shouldYield)
                {
                    if (Constants.SteppedOutEnemyTurnsCameraFollowMovement && moveDistance >= Constants.SteppedOutEnemyTurnsCameraFollowThreshold)
                    {
                        UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                        while (UICameraController.Instance.IsCameraSmoothing())
                        {
                            yield return null;
                        }
                    }

                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
}