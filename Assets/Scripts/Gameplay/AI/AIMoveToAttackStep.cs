using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMoveToAttackStep : AIStep
{
    public override void TakeStep()
    {
        if (m_AIGameEnemyEntity.m_targetToAttack == null)
        {
            MoveTowardsCastle();
            return;
        }

        GameTile targetTile = null;
        switch(m_AIGameEnemyEntity.m_targetToAttack)
        {
            case GameEntity gameEntity:
                targetTile = gameEntity.m_curTile;
                break;
            case GameBuildingBase gameBuildingBase:
                targetTile = gameBuildingBase.m_curTile.GetGameTile();
                break;
        }
        if (targetTile == null)
        {
            MoveTowardsCastle();
            return;
        }

        List<GameTile> tilesInMoveAttackRange = WorldGridManager.Instance.GetTilesInMoveAttackRange(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, false);
        List<GameTile> tilesInRangeToAttack = WorldGridManager.Instance.GetSurroundingTiles(targetTile, m_AIGameEnemyEntity.m_gameEnemyEntity.GetRange());

        List<GameTile> tilesToMoveTo = tilesInMoveAttackRange.Where(t => tilesInRangeToAttack.Contains(t)).ToList();

        int closestTile = tilesToMoveTo.Min(t => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, t));
        GameTile moveDestination = tilesToMoveTo.Single(t => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, t) == closestTile);

        m_AIGameEnemyEntity.m_gameEnemyEntity.MoveTo(moveDestination);
    }

    private void MoveTowardsCastle()
    {

    }
}