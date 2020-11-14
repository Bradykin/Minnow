using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AILordOfEruptionsMoveToTargetStep : AIMoveToTargetStandardStep
{
    private ContentLordOfEruptionsEnemy lordOfEruptionsEnemy;

    public AILordOfEruptionsMoveToTargetStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit)
    {
        if (!(m_AIGameEnemyUnit.m_gameEnemyUnit is ContentLordOfEruptionsEnemy))
        {
            Debug.LogError("Wrong unit using Lord of Eruptions AI script.");
        }

        lordOfEruptionsEnemy = (ContentLordOfEruptionsEnemy)m_AIGameEnemyUnit.m_gameEnemyUnit;
    }

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

        List<GameTile> tilesInMoveAttackRange = WorldGridManager.Instance.GetSurroundingGameTiles(lordOfEruptionsEnemy.GetGameTile(), lordOfEruptionsEnemy.m_teleportRange);
        List<GameTile> tilesInRangeToAttack = WorldGridManager.Instance.GetSurroundingGameTiles(targetTile, m_AIGameEnemyUnit.m_gameEnemyUnit.GetRange());

        List<GameTile> tilesToMoveTo = tilesInMoveAttackRange.Where(t => (t == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile() || !t.IsOccupied() || t.m_occupyingUnit.m_isDead) && tilesInRangeToAttack.Contains(t)).ToList();

        if (tilesToMoveTo.Count == 0 || tilesToMoveTo.Contains(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile()))
        {
            yield break;
        }

        int closestTileDistance = tilesToMoveTo.Min(t => WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), t, true, false, false));
        GameTile moveDestination;
        List<GameTile> closestGameTiles = tilesToMoveTo.Where(t => WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), t, true, false, false) == closestTileDistance).ToList();

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
        m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination, false);

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

        List<GameTile> tilesInMoveAttackRange = WorldGridManager.Instance.GetSurroundingGameTiles(lordOfEruptionsEnemy.GetGameTile(), lordOfEruptionsEnemy.m_teleportRange);
        List<GameTile> tilesInRangeToAttack = WorldGridManager.Instance.GetSurroundingGameTiles(targetTile, m_AIGameEnemyUnit.m_gameEnemyUnit.GetRange());

        List<GameTile> tilesToMoveTo = tilesInMoveAttackRange.Where(t => (t == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile() || !t.IsOccupied() || t.m_occupyingUnit.m_isDead) && tilesInRangeToAttack.Contains(t)).ToList();

        if (tilesToMoveTo.Count == 0 || tilesToMoveTo.Contains(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile()))
        {
            return;
        }

        int closestTileDistance = tilesToMoveTo.Min(t => WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), t, true, false, false));
        GameTile moveDestination;
        List<GameTile> closestGameTiles = tilesToMoveTo.Where(t => WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), t, true, false, false) == closestTileDistance).ToList();

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
        m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination, false);
    }

    protected override IEnumerator MoveTowardsCastleCoroutine()
    {
        int amountStaminaToSpend = lordOfEruptionsEnemy.m_teleportRange;

        if (GameHelper.GetPlayer() == null || GameHelper.GetPlayer().GetCastleGameElement() == null)
        {
            yield break;
        }

        if (m_AIGameEnemyUnit.m_targetGameTile == null)
        {
            m_AIGameEnemyUnit.m_targetGameTile = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GameHelper.GetPlayer().GetCastleGameTile(), amountStaminaToSpend, true);
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
        m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination, false);

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

    protected override void MoveTowardsCastleInstant()
    {
        int amountStaminaToSpend = lordOfEruptionsEnemy.m_teleportRange;

        if (GameHelper.GetPlayer() == null || GameHelper.GetPlayer().GetCastleGameElement() == null)
        {
            return;
        }

        if (m_AIGameEnemyUnit.m_targetGameTile == null)
        {
            m_AIGameEnemyUnit.m_targetGameTile = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GameHelper.GetPlayer().GetCastleGameTile(), amountStaminaToSpend, true);
        }
        GameTile moveDestination = m_AIGameEnemyUnit.m_targetGameTile;

        if (moveDestination != m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile())
        {
            m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination, false);
        }
    }
}