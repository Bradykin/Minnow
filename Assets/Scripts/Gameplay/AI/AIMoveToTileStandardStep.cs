using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMoveToTileStandardStep : AIMoveStep
{
    public AIMoveToTileStandardStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep(bool shouldYield)
    {
        GameTile moveDestination = m_AIGameEnemyUnit.m_targetGameTile;

        if (moveDestination == null)
        {
            if (shouldYield)
            {
                yield return FactoryManager.Instance.StartCoroutine(MoveTowardsCastle(shouldYield, m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen()));
            }
            else
            {
                FactoryManager.Instance.StartCoroutine(MoveTowardsCastle(shouldYield, m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen()));
            }
            yield break;
        }

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