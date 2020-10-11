using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMoveToTargetStandardStep : AIMoveStep
{
    public AIMoveToTargetStandardStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep(bool yield)
    {
        if (yield)
        {
            yield return FactoryManager.Instance.StartCoroutine(MoveToTarget(yield, m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen(), false));
        }
        else
        {
            FactoryManager.Instance.StartCoroutine(MoveToTarget(yield, m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen(), false));
        }
    }

    protected IEnumerator MoveToTarget(bool yield, int staminaUsageToMoveToCastle, bool letPassEnemies)
    {
        if (m_AIGameEnemyUnit.m_targetGameElement == null)
        {
            if (yield)
            {
                yield return FactoryManager.Instance.StartCoroutine(MoveTowardsCastle(yield, staminaUsageToMoveToCastle));
            }
            else
            {
                FactoryManager.Instance.StartCoroutine(MoveTowardsCastle(yield, staminaUsageToMoveToCastle));
            }
            yield break;
        }

        GameTile targetTile = null;
        switch (m_AIGameEnemyUnit.m_targetGameElement)
        {
            case GameUnit gameUnit:
                targetTile = gameUnit.GetGameTile();
                if (m_AIGameEnemyUnit.m_gameEnemyUnit.IsInRangeOfUnit(gameUnit))
                {
                    yield break;
                }
                break;
            case GameBuildingBase gameBuilding:
                targetTile = gameBuilding.GetGameTile();
                if (m_AIGameEnemyUnit.m_gameEnemyUnit.IsInRangeOfBuilding(gameBuilding))
                {
                    yield break;
                }
                break;
        }
        if (targetTile == null)
        {
            if (yield)
            {
                yield return FactoryManager.Instance.StartCoroutine(MoveTowardsCastle(yield, staminaUsageToMoveToCastle));
            }
            else
            {
                FactoryManager.Instance.StartCoroutine(MoveTowardsCastle(yield, staminaUsageToMoveToCastle));
            }
            yield break;
        }

        List<GameTile> tilesInMoveAttackRange = WorldGridManager.Instance.GetTilesInMovementRangeWithStaminaToAttack(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), false, letPassEnemies);
        List<GameTile> tilesInRangeToAttack = WorldGridManager.Instance.GetSurroundingTiles(targetTile, m_AIGameEnemyUnit.m_gameEnemyUnit.GetRange());

        List<GameTile> tilesToMoveTo = tilesInMoveAttackRange.Where(t => (t == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile() || !t.IsOccupied() || t.m_occupyingUnit.m_isDead) && tilesInRangeToAttack.Contains(t)).ToList();

        if (tilesToMoveTo.Count == 0 || tilesToMoveTo.Contains(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile()))
        {
            yield break;
        }

        int closestTile = tilesToMoveTo.Min(t => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), t));
        GameTile moveDestination = tilesToMoveTo.First(t => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), t) == closestTile);
        m_AIGameEnemyUnit.m_targetGameTile = moveDestination;

        if (moveDestination == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile())
        {
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

            yield return new WaitForSeconds(0.5f);
        }
    }
}