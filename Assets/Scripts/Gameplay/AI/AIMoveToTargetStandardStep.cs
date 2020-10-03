using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMoveToTargetStandardStep : AIMoveStep
{
    public AIMoveToTargetStandardStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override void TakeStep()
    {
        MoveToTarget(m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen(), false);
    }

    protected void MoveToTarget(int staminaUsageToMoveToCastle, bool letPassEnemies)
    {
        if (m_AIGameEnemyUnit.m_targetGameElement == null)
        {
            MoveTowardsCastle(m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen());
            return;
        }

        GameTile targetTile = null;
        switch (m_AIGameEnemyUnit.m_targetGameElement)
        {
            case GameUnit gameUnit:
                targetTile = gameUnit.GetGameTile();
                break;
            case GameBuildingBase gameBuildingBase:
                targetTile = gameBuildingBase.GetGameTile();
                break;
        }
        if (targetTile == null)
        {
            MoveTowardsCastle(staminaUsageToMoveToCastle);
            return;
        }

        List<GameTile> tilesInMoveAttackRange = WorldGridManager.Instance.GetTilesInMovementRangeWithStaminaToAttack(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), false, letPassEnemies);
        List<GameTile> tilesInRangeToAttack = WorldGridManager.Instance.GetSurroundingTiles(targetTile, m_AIGameEnemyUnit.m_gameEnemyUnit.GetRange());

        List<GameTile> tilesToMoveTo = tilesInMoveAttackRange.Where(t => (t == m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile() || !t.IsOccupied() || t.m_occupyingUnit.m_isDead) && tilesInRangeToAttack.Contains(t)).ToList();

        if (tilesToMoveTo.Count == 0 || tilesToMoveTo.Contains(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile()))
        {
            return;
        }

        int closestTile = tilesToMoveTo.Min(t => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), t));
        GameTile moveDestination = tilesToMoveTo.First(t => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), t) == closestTile);

        m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination);
    }
}