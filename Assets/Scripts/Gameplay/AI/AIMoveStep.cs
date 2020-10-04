using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AIMoveStep : AIStep
{
    public AIMoveStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    protected IEnumerator MoveTowardsCastle(bool yield, int amountStaminaToSpend)
    {
        if (GameHelper.GetPlayer() != null && GameHelper.GetPlayer().Castle != null)
        {
            GameTile moveDestination = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GameHelper.GetPlayer().Castle.GetGameTile(), amountStaminaToSpend);
            m_AIGameEnemyUnit.m_targetGameTile = moveDestination;

            if (moveDestination != m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile())
            {
                bool useSteppedOutTurn = yield && m_AIGameEnemyUnit.UseSteppedOutTurn;

                if (useSteppedOutTurn)
                {
                    UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                    while (UICameraController.Instance.IsCameraSmoothing())
                    {
                        yield return null;
                    }
                }

                int moveDistance = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), moveDestination, true, false, true);
                m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination);

                if (useSteppedOutTurn)
                {
                    if (Constants.SteppedOutEnemyTurnsCameraFollowMovement && moveDistance >= Constants.SteppedOutEnemyTurnsCameraFollowThreshold)
                    {
                        UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                        while (UICameraController.Instance.IsCameraSmoothing())
                        {
                            yield return null;
                        }
                    }

                    //UIHelper.CreateWorldElementNotification("Does AI step: " + GetType(), true, m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
}