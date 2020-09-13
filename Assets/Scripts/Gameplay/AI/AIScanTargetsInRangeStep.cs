using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScanTargetsInRangeStep : AIStep
{
    public AIScanTargetsInRangeStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }
    
    public override void TakeStep()
    {
        List<GameTile> tilesInAttackRange = WorldGridManager.Instance.GetTilesInAttackRange(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, false);

        if (tilesInAttackRange == null)
            return;

        List<GameEntity> possibleEntityTargets = new List<GameEntity>();
        List<GameBuildingBase> possibleBuildingTargets = new List<GameBuildingBase>();

        foreach (var tile in tilesInAttackRange)
        {
            if (tile.m_occupyingEntity != null && tile.m_occupyingEntity.GetTeam() == Team.Player)
            {
                possibleEntityTargets.Add(tile.m_occupyingEntity);
            }

            if (tile.HasBuilding() && !tile.GetBuilding().m_isDestroyed)
            {
                possibleBuildingTargets.Add(tile.GetBuilding());
            }
        }

        m_AIGameEnemyEntity.m_possibleEntityTargets = possibleEntityTargets;
        m_AIGameEnemyEntity.m_possibleBuildingTargets = possibleBuildingTargets;
    }
}
