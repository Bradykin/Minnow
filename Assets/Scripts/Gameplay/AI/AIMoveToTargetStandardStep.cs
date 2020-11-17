using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMoveToTargetStandardStep : AIMoveStep
{
    protected bool letPassEnemies = false;

    public AIMoveToTargetStandardStep(AIGameEnemyUnit AIGameEnemyUnit, int numTurnsDelayMovement = 0) : base(AIGameEnemyUnit, numTurnsDelayMovement) { }

    public override IEnumerator TakeStepCoroutine()
    {
        if (m_AIGameEnemyUnit.m_targetGameElement == null)
        {
            yield return FactoryManager.Instance.StartCoroutine(MoveTowardsCastleCoroutine());
            yield break;
        }

        GameTile targetTile = null;
        switch (m_AIGameEnemyUnit.m_targetGameElement)
        {
            case GameUnit gameUnit:
                targetTile = gameUnit.GetGameTile();
                break;
            case GameBuildingBase gameBuilding:
                targetTile = gameBuilding.GetGameTile();
                break;
        }

        if (targetTile == null)
        {
            yield return FactoryManager.Instance.StartCoroutine(MoveTowardsCastleCoroutine());
            yield break;
        }

        if (m_AIGameEnemyUnit.m_gameEnemyUnit.IsInRangeOfTile(targetTile))
        {
            //Already in range, don't need to move
            yield break;
        }

        List<GameTile> tilesInMoveAttackRange = WorldGridManager.Instance.GetTilesInMovementRangeWithStaminaToAttack(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), false, letPassEnemies);
        List<GameTile> tilesInRangeToAttack = WorldGridManager.Instance.GetSurroundingGameTiles(targetTile, m_AIGameEnemyUnit.m_gameEnemyUnit.GetRange());

        List<GameTile> tilesToMoveTo = tilesInMoveAttackRange.Where(t => (t == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile() || !t.IsOccupied() || t.m_occupyingUnit.m_isDead) && tilesInRangeToAttack.Contains(t)).ToList();

        if (tilesToMoveTo.Count == 0 || tilesToMoveTo.Contains(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile()))
        {
            yield break;
        }

        int closestTileDistance = tilesToMoveTo.Min(t => WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), t, false, false, false));
        GameTile moveDestination;
        List<GameTile> closestGameTiles = tilesToMoveTo.Where(t => WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), t, false, false, false) == closestTileDistance).ToList();

        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetFlyingKeyword() != null && closestGameTiles.Any(t => t.GetTerrain().IsMountain() || t.GetTerrain().IsWater()))
        {
            moveDestination = closestGameTiles.FirstOrDefault(t => t.GetTerrain().IsMountain() || t.GetTerrain().IsWater());
        }
        else
        {
            moveDestination = closestGameTiles[Random.Range(0, closestGameTiles.Count)];
        }
        m_AIGameEnemyUnit.m_targetGameTile = moveDestination;

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

    public override void TakeStepInstant()
    {
        if (m_AIGameEnemyUnit.m_targetGameElement == null)
        {
            MoveTowardsCastleInstant();
            return;
        }

        GameTile targetTile = null;
        switch (m_AIGameEnemyUnit.m_targetGameElement)
        {
            case GameUnit gameUnit:
                targetTile = gameUnit.GetGameTile();
                break;
            case GameBuildingBase gameBuilding:
                targetTile = gameBuilding.GetGameTile();
                break;
        }

        if (targetTile == null)
        {
            MoveTowardsCastleInstant();
            return;
        }

        if (m_AIGameEnemyUnit.m_gameEnemyUnit.IsInRangeOfTile(targetTile))
        {
            //Already in range, don't need to move
            return;
        }

        List<GameTile> tilesInMoveAttackRange = WorldGridManager.Instance.GetTilesInMovementRangeWithStaminaToAttack(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), false, letPassEnemies);
        List<GameTile> tilesInRangeToAttack = WorldGridManager.Instance.GetSurroundingGameTiles(targetTile, m_AIGameEnemyUnit.m_gameEnemyUnit.GetRange());

        List<GameTile> tilesToMoveTo = tilesInMoveAttackRange.Where(t => (t == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile() || !t.IsOccupied() || t.m_occupyingUnit.m_isDead) && tilesInRangeToAttack.Contains(t)).ToList();

        if (tilesToMoveTo.Count == 0 || tilesToMoveTo.Contains(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile()))
        {
            return;
        }

        int closestTileDistance = tilesToMoveTo.Min(t => WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), t, false, false, false));
        GameTile moveDestination;
        List<GameTile> closestGameTiles = tilesToMoveTo.Where(t => WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), t, false, false, false) == closestTileDistance).ToList();

        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetFlyingKeyword() != null && closestGameTiles.Any(t => t.GetTerrain().IsMountain() || t.GetTerrain().IsWater()))
        {
            moveDestination = closestGameTiles.FirstOrDefault(t => t.GetTerrain().IsMountain() || t.GetTerrain().IsWater());
        }
        else
        {
            moveDestination = closestGameTiles[Random.Range(0, closestGameTiles.Count)];
        }
        m_AIGameEnemyUnit.m_targetGameTile = moveDestination;

        if (moveDestination == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile())
        {
            return;
        }

        int moveDistance = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), moveDestination, true, false, true);
        m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination);
    }
}