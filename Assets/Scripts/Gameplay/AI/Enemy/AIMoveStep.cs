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

        SetTargetGameTileInMoveTowardsCastle(amountStaminaToSpend);
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

        SetTargetGameTileInMoveTowardsCastle(amountStaminaToSpend);
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

    private void SetTargetGameTileInMoveTowardsCastle(int amountStaminaToSpend)
    {
        m_AIGameEnemyUnit.m_targetGameTile = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GetCastleTravelTile(), amountStaminaToSpend);

        GameTile moveDestination = m_AIGameEnemyUnit.m_targetGameTile;

        if (moveDestination.GetTerrain().GetCoverType() == GameTerrainBase.CoverType.Cover)
        {
            return;
        }

        List<GameTile> areaSurroundingDestination = WorldGridManager.Instance.GetSurroundingGameTiles(moveDestination, 4, 0);
        if (areaSurroundingDestination.Any(t => t.IsOccupied() && t.GetOccupyingUnit().GetTeam() == Team.Player))
        {
            List<GameTile> candidateMoveLocations = WorldGridManager.Instance.GetSurroundingGameTiles(moveDestination, 2, 0)
                .Where(t => t != m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile() 
                && t.GetTerrain().GetCoverType() == GameTerrainBase.CoverType.Cover
                && m_AIGameEnemyUnit.m_gameEnemyUnit.CanMoveTo(t)).ToList();

            candidateMoveLocations.OrderBy(t => WorldGridManager.Instance.GetPathLength(t, GetCastleTravelTile(), false, true, true, m_AIGameEnemyUnit.m_gameEnemyUnit));

            if (candidateMoveLocations.Count > 0)
            {
                int numOne = WorldGridManager.Instance.GetPathLength(candidateMoveLocations[0], GetCastleTravelTile(), false, true, true, m_AIGameEnemyUnit.m_gameEnemyUnit);
                int numTwo = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), GetCastleTravelTile(), false, true, true);

                if (numOne < numTwo)
                {
                    m_AIGameEnemyUnit.m_targetGameTile = candidateMoveLocations[0];
                }
            }
        }
    }

    public override void CleanupAIStep()
    {
        if (m_numTurnsDelayMovement > 0)
        {
            m_numTurnsDelayMovement -= 1;
        }
    }
}