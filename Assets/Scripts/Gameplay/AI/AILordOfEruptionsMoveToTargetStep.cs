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

        List<GameTile> tilesInMoveAttackRange = WorldGridManager.Instance.GetSurroundingGameTiles(lordOfEruptionsEnemy.GetGameTile(), lordOfEruptionsEnemy.m_teleportRange);
        List<GameTile> tilesInRangeToAttack = WorldGridManager.Instance.GetSurroundingGameTiles(targetTile, m_AIGameEnemyUnit.m_gameEnemyUnit.GetRange());
        List<GameTile> tilesInMoveAdjacentRangeThatAreVolcanoes = WorldGridManager.Instance.GetSurroundingGameTiles(lordOfEruptionsEnemy.GetGameTile(), lordOfEruptionsEnemy.m_teleportRange, 0).Where(t => WorldGridManager.Instance.GetSurroundingGameTiles(t, 1, 0).Any(ter => ter.GetTerrain() is ContentVolcanoInactiveTerrain)).ToList();

        List<GameTile> tilesToMoveTo = tilesInMoveAttackRange.Where(t => (t == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile() || !t.IsOccupied() || t.m_occupyingUnit.m_isDead) && tilesInRangeToAttack.Contains(t)).ToList();
        List<GameTile> tilesToMoveToNearVolcanoes = tilesToMoveTo.Where(t => tilesInMoveAdjacentRangeThatAreVolcanoes.Contains(t)).ToList();

        if (tilesToMoveTo.Count == 0)
        {
            yield break;
        }

        if (tilesToMoveToNearVolcanoes.Count > 0)
        {
            tilesToMoveTo.Clear();
            tilesToMoveTo = tilesToMoveToNearVolcanoes;
        }

        GameTile moveDestination = FindDestinationTileFarthestFromThreats(tilesToMoveTo);
        m_AIGameEnemyUnit.m_targetGameTile = moveDestination;

        if (moveDestination == null || moveDestination == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile())
        {
            yield break; ;
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

        List<GameTile> tilesInMoveAttackRange = WorldGridManager.Instance.GetSurroundingGameTiles(lordOfEruptionsEnemy.GetGameTile(), lordOfEruptionsEnemy.m_teleportRange);
        List<GameTile> tilesInRangeToAttack = WorldGridManager.Instance.GetSurroundingGameTiles(targetTile, m_AIGameEnemyUnit.m_gameEnemyUnit.GetRange());
        List<GameTile> tilesInMoveAdjacentRangeThatAreVolcanoes = WorldGridManager.Instance.GetSurroundingGameTiles(lordOfEruptionsEnemy.GetGameTile(), lordOfEruptionsEnemy.m_teleportRange, 0).Where(t => WorldGridManager.Instance.GetSurroundingGameTiles(t, 1, 0).Any(ter => ter.GetTerrain() is ContentVolcanoInactiveTerrain)).ToList();

        List<GameTile> tilesToMoveTo = tilesInMoveAttackRange.Where(t => (t == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile() || !t.IsOccupied() || t.m_occupyingUnit.m_isDead) && tilesInRangeToAttack.Contains(t)).ToList();
        List<GameTile> tilesToMoveToNearVolcanoes = tilesToMoveTo.Where(t => tilesInMoveAdjacentRangeThatAreVolcanoes.Contains(t)).ToList();

        if (tilesToMoveTo.Count == 0)
        {
            return;
        }

        if (tilesToMoveToNearVolcanoes.Count > 0)
        {
            tilesToMoveTo.Clear();
            tilesToMoveTo = tilesToMoveToNearVolcanoes;
        }

        GameTile moveDestination = FindDestinationTileFarthestFromThreats(tilesToMoveTo);
        m_AIGameEnemyUnit.m_targetGameTile = moveDestination;

        if (moveDestination == null || moveDestination == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile())
        {
            return;
        }

        int moveDistance = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), moveDestination, true, false, true);
        m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination, false);
    }

    private GameTile FindDestinationTileFarthestFromThreats(List<GameTile> possibleTiles)
    {
        if (possibleTiles.Count == 0)
        {
            return null;
        }

        if (possibleTiles.Count == 1)
        {
            return possibleTiles[0];
        }

        for (int i = 2; i > 0; i--)
        {
            int minNumThreats = possibleTiles.Min(t => FindNumThreatsInAreaAroundTile(t, i));

            if (minNumThreats == 0 || i == 1)
            {
                List<GameTile> noThreatTiles = possibleTiles.Where(t => FindNumThreatsInAreaAroundTile(t, i) == 0).ToList();
                return noThreatTiles[Random.Range(0, noThreatTiles.Count)];
            }
        }

        Debug.LogError("Code reached a point it never should.");
        return possibleTiles[0];
    }

    private int FindNumThreatsInAreaAroundTile(GameTile tile, int range)
    {
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(tile, range);
        int numThreats = 0;

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (surroundingTiles[i].IsOccupied() && surroundingTiles[i].m_occupyingUnit.GetTeam() == Team.Player)
            {
                numThreats++;
            }
        }

        return numThreats;
    }

    protected override IEnumerator MoveTowardsCastleCoroutine()
    {
        if (GameHelper.GetPlayer() == null || GameHelper.GetPlayer().GetCastleGameElement() == null)
        {
            yield break;
        }

        if (m_AIGameEnemyUnit.m_targetGameTile == null)
        {
            SetTargetGameTileInMoveTowardsCastle();
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
        if (GameHelper.GetPlayer() == null || GameHelper.GetPlayer().GetCastleGameElement() == null)
        {
            return;
        }

        if (m_AIGameEnemyUnit.m_targetGameTile == null)
        {
            SetTargetGameTileInMoveTowardsCastle();
        }
        GameTile moveDestination = m_AIGameEnemyUnit.m_targetGameTile;

        if (moveDestination != m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile())
        {
            m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination, false);
        }
    }

    private void SetTargetGameTileInMoveTowardsCastle()
    {
        List<GameTile> nearbyInactiveVolcanoes = FindNearbyInactiveVolcanoes();
        int amountDistanceToMove = lordOfEruptionsEnemy.m_teleportRange;

        if (nearbyInactiveVolcanoes.Count > 0)
        {
            int minDistanceToVolcano = nearbyInactiveVolcanoes.Min(t => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(lordOfEruptionsEnemy.GetGameTile(), t));
            GameTile targetVolcano = nearbyInactiveVolcanoes.FirstOrDefault(t => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(lordOfEruptionsEnemy.GetGameTile(), t) == minDistanceToVolcano);
            m_AIGameEnemyUnit.m_targetGameTile = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(targetVolcano, amountDistanceToMove, true);
        }
        else
        {
            m_AIGameEnemyUnit.m_targetGameTile = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GameHelper.GetPlayer().GetCastleGameTile(), amountDistanceToMove, true);
        }
    }

    private List<GameTile> FindNearbyInactiveVolcanoes()
    {
        return WorldGridManager.Instance.GetSurroundingGameTiles(lordOfEruptionsEnemy.GetGameTile(), 7, 0).Where(t => t.GetTerrain() is ContentVolcanoInactiveTerrain).ToList();
    }
}