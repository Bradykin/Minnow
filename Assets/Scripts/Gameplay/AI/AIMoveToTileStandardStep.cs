using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMoveToTileStandardStep : AIMoveStep
{
    public AIMoveToTileStandardStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep(bool yield)
    {
        GameTile moveDestination = m_AIGameEnemyUnit.m_targetGameTile;

        if (moveDestination == null)
        {
            if (yield)
            {
                yield return FactoryManager.Instance.StartCoroutine(MoveTowardsCastle(yield, m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen()));
            }
            else
            {
                FactoryManager.Instance.StartCoroutine(MoveTowardsCastle(yield, m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen()));
            }
            yield break;
        }

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