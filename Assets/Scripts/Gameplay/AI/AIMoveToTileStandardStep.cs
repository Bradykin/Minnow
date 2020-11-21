using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMoveToTileStandardStep : AIMoveStep
{
    public AIMoveToTileStandardStep(AIGameEnemyUnit AIGameEnemyUnit, int numTurnsDelayMovement = 0) : base(AIGameEnemyUnit, numTurnsDelayMovement) { }

    public override IEnumerator TakeStepCoroutine()
    {
        GameTile moveDestination = m_AIGameEnemyUnit.m_targetGameTile;
        if (moveDestination == null)
        {
            yield return FactoryManager.Instance.StartCoroutine(MoveTowardsCastleCoroutine());
            yield break;
        }

        UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
        while (UICameraController.Instance.IsCameraSmoothing())
        {
            yield return null;
        }

        int moveDistance = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), moveDestination, true, false, true);
        m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination);

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

    public override void TakeStepInstant()
    {
        GameTile moveDestination = m_AIGameEnemyUnit.m_targetGameTile;
        if (moveDestination == null)
        {
            MoveTowardsCastleInstant();
            return;
        }

        m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination);
    }
}