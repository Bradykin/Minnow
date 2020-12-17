using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AIMoveStep : AIStep
{
    private int m_numTurnsDelayMovement;

    public AIMoveStep(AIGameEnemyUnit AIGameEnemyUnit, int numTurnsDelayMovement = 0) : base(AIGameEnemyUnit) 
    {
        m_numTurnsDelayMovement = numTurnsDelayMovement;
    }

    protected virtual IEnumerator MoveTowardsCastleCoroutine()
    {
        if (m_numTurnsDelayMovement > 0)
        {
            yield break;
        }
        
        int amountStaminaToSpend = GetStaminaToUseToMoveToCastle();
        
        if (GameHelper.GetPlayer() == null || GameHelper.GetPlayer().GetCastleGameElement() == null)
        {
            yield break;
        }

        m_AIGameEnemyUnit.m_targetGameTile = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GetCastleTravelTile(), amountStaminaToSpend);
        GameTile moveDestination = m_AIGameEnemyUnit.m_targetGameTile;

        if (moveDestination == null || moveDestination == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile())
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
        if (m_numTurnsDelayMovement > 0)
        {
            return;
        }

        int amountStaminaToSpend = GetStaminaToUseToMoveToCastle();
        
        if (GameHelper.GetPlayer() == null || GameHelper.GetPlayer().GetCastleGameElement() == null)
        {
            return;
        }

        m_AIGameEnemyUnit.m_targetGameTile = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GetCastleTravelTile(), amountStaminaToSpend);
        GameTile moveDestination = m_AIGameEnemyUnit.m_targetGameTile;

        if (moveDestination != null && moveDestination != m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile())
        {
            m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination);
        }
    }

    protected virtual GameTile GetCastleTravelTile()
    {
        return GameHelper.GetPlayer().GetCastleGameTile();
    }

    protected virtual int GetStaminaToUseToMoveToCastle()
    {
        return Mathf.Min(m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina(), m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen());
    }

    public override void CleanupAIStep()
    {
        if (m_numTurnsDelayMovement > 0)
        {
            m_numTurnsDelayMovement -= 1;
        }
    }
}