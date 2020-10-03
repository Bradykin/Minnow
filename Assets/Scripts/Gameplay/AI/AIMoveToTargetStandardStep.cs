using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMoveToTargetStandardStep : AIMoveStep
{
    public AIMoveToTargetStandardStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override IEnumerator TakeStep()
    {
        yield return FactoryManager.Instance.StartCoroutine(MoveToTarget(m_AIGameEnemyEntity.m_gameEnemyEntity.GetAPRegen(), false));
    }

    protected IEnumerator MoveToTarget(int apUsageToMoveToCastle, bool letPassEnemies)
    {
        if (m_AIGameEnemyEntity.m_targetGameElement == null)
        {
            yield return FactoryManager.Instance.StartCoroutine(MoveTowardsCastle(m_AIGameEnemyEntity.m_gameEnemyEntity.GetAPRegen()));
            yield break;
        }

        GameTile targetTile = null;
        switch (m_AIGameEnemyEntity.m_targetGameElement)
        {
            case GameEntity gameEntity:
                targetTile = gameEntity.GetGameTile();
                if (m_AIGameEnemyEntity.m_gameEnemyEntity.IsInRangeOfEntity(gameEntity))
                {
                    yield break;
                }
                break;
            case GameBuildingBase gameBuilding:
                targetTile = gameBuilding.GetGameTile();
                if (m_AIGameEnemyEntity.m_gameEnemyEntity.IsInRangeOfBuilding(gameBuilding))
                {
                    yield break;
                }
                break;
        }
        if (targetTile == null)
        {
            yield return FactoryManager.Instance.StartCoroutine(MoveTowardsCastle(apUsageToMoveToCastle));
            yield break;
        }

        List<GameTile> tilesInMoveAttackRange = WorldGridManager.Instance.GetTilesInMovementRangeWithAPToAttack(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), false, letPassEnemies);
        List<GameTile> tilesInRangeToAttack = WorldGridManager.Instance.GetSurroundingTiles(targetTile, m_AIGameEnemyEntity.m_gameEnemyEntity.GetRange());

        List<GameTile> tilesToMoveTo = tilesInMoveAttackRange.Where(t => (t == m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile() || !t.IsOccupied() || t.m_occupyingEntity.m_isDead) && tilesInRangeToAttack.Contains(t)).ToList();

        if (tilesToMoveTo.Count == 0 || tilesToMoveTo.Contains(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile()))
        {
            yield break;
        }

        int closestTile = tilesToMoveTo.Min(t => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), t));
        GameTile moveDestination = tilesToMoveTo.First(t => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), t) == closestTile);
        m_AIGameEnemyEntity.m_targetGameTile = moveDestination;

        if (moveDestination == m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile())
        {
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