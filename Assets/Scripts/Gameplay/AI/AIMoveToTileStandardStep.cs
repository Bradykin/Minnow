using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMoveToTileStandardStep : AIMoveStep
{
    public AIMoveToTileStandardStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override IEnumerator TakeStep()
    {
        GameTile moveDestination = m_AIGameEnemyEntity.m_targetGameTile;

        if (moveDestination == null)
        {
            yield return FactoryManager.Instance.StartCoroutine(MoveTowardsCastle(m_AIGameEnemyEntity.m_gameEnemyEntity.GetAPRegen()));
            yield break;
        }

        bool useSteppedOutTurn = m_AIGameEnemyEntity.UseSteppedOutTurn;

        if (useSteppedOutTurn)
        {
            UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyEntity.m_gameEnemyEntity.GetWorldTile().gameObject);
            while (UICameraController.Instance.IsCameraSmoothing())
            {
                yield return null;
            }
        }

        int moveDistance = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), moveDestination, true, false, true);
        m_AIGameEnemyEntity.m_gameEnemyEntity.m_uiEntity.MoveTo(moveDestination);

        if (useSteppedOutTurn)
        {
            if (Constants.SteppedOutEnemyTurnsCameraFollowMovement && moveDistance >= Constants.SteppedOutEnemyTurnsCameraFollowThreshold)
            {
                UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyEntity.m_gameEnemyEntity.GetWorldTile().gameObject);
                while (UICameraController.Instance.IsCameraSmoothing())
                {
                    yield return null;
                }
            }

            UIHelper.CreateWorldElementNotification("Does AI step: " + GetType(), true, m_AIGameEnemyEntity.m_gameEnemyEntity.GetWorldTile().gameObject);
            yield return new WaitForSeconds(0.5f);
        }
    }
}