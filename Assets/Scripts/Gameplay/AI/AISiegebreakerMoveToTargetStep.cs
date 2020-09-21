using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AISiegebreakerMoveToTargetStep : AIMoveStep
{
    public AISiegebreakerMoveToTargetStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override void TakeStep()
    {
        if (m_AIGameEnemyEntity.m_targetGameElement == null)
        {
            MoveTowardsCastle(m_AIGameEnemyEntity.m_gameEnemyEntity.GetCurAP());
            return;
        }

        GameTile targetTile = null;
        switch(m_AIGameEnemyEntity.m_targetGameElement)
        {
            case GameEntity gameEntity:
                targetTile = gameEntity.GetGameTile();
                break;
            case GameBuildingBase gameBuildingBase:
                targetTile = gameBuildingBase.GetGameTile();
                break;
        }
        if (targetTile == null)
        {
            MoveTowardsCastle(m_AIGameEnemyEntity.m_gameEnemyEntity.GetCurAP());
            return;
        }

        List<GameTile> tilesInMoveAttackRange = WorldGridManager.Instance.GetTilesInMoveAttackRange(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), false, true);
        List<GameTile> tilesInRangeToAttack = WorldGridManager.Instance.GetSurroundingTiles(targetTile, m_AIGameEnemyEntity.m_gameEnemyEntity.GetRange());

        List<GameTile> tilesToMoveTo = tilesInMoveAttackRange.Where(t => (t == m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile() || !t.IsOccupied() || t.m_occupyingEntity.m_isDead) && tilesInRangeToAttack.Contains(t)).ToList();

        if (tilesToMoveTo.Count == 0 || tilesToMoveTo.Contains(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile()))
        {
            return;
        }

        int closestTile = tilesToMoveTo.Min(t => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), t));
        GameTile moveDestination = tilesToMoveTo.First(t => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), t) == closestTile);

        m_AIGameEnemyEntity.m_gameEnemyEntity.m_uiEntity.MoveTo(moveDestination);
    }
}