using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AIMoveStep : AIStep
{
    public AIMoveStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    protected virtual IEnumerator MoveTowardsCastleCoroutine()
    {
        int amountStaminaToSpend = GetStaminaToUseToMoveToCastle();
        
        if (GameHelper.GetPlayer() == null || GameHelper.GetPlayer().GetCastleGameElement() == null)
        {
            yield break;
        }

        if (m_AIGameEnemyUnit.m_targetGameTile == null)
        {
            m_AIGameEnemyUnit.m_targetGameTile = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GameHelper.GetPlayer().GetCastleGameTile(), amountStaminaToSpend);
        }
        GameTile moveDestination = m_AIGameEnemyUnit.m_targetGameTile;

        if (moveDestination == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile())
        {
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

    protected virtual void MoveTowardsCastleInstant()
    {
        int amountStaminaToSpend = GetStaminaToUseToMoveToCastle();
        
        if (GameHelper.GetPlayer() == null || GameHelper.GetPlayer().GetCastleGameElement() == null)
        {
            return;
        }

        if (m_AIGameEnemyUnit.m_targetGameTile == null)
        {
            m_AIGameEnemyUnit.m_targetGameTile = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GameHelper.GetPlayer().GetCastleGameTile(), amountStaminaToSpend);
        }
        GameTile moveDestination = m_AIGameEnemyUnit.m_targetGameTile;

        if (moveDestination != m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile())
        {
            m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination);
        }
    }

    protected virtual int GetStaminaToUseToMoveToCastle()
    {
        return Mathf.Min(m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina(), m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen());
    }
}