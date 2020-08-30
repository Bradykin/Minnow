﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScanTargetsInRangeStep : AIStep
{
    public AIScanTargetsInRangeStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }
    
    public override void TakeStep()
    {
        List<GameTile> tilesInAttackRange = WorldGridManager.Instance.GetTilesInAttackRange(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, false);

        List<GameEntity> possibleEntityTargets = new List<GameEntity>();
        List<GameBuildingBase> possibleBuildingTargets = new List<GameBuildingBase>();

        foreach (var tile in tilesInAttackRange)
        {
            if (tile.m_occupyingEntity != null && tile.m_occupyingEntity.GetTeam() == Team.Player && m_AIGameEnemyEntity.m_gameEnemyEntity.CanHitEntity(tile.m_occupyingEntity))
            {
                possibleEntityTargets.Add(tile.m_occupyingEntity);
            }

            if (tile.HasBuilding())
            {
                possibleBuildingTargets.Add(tile.GetBuilding());
            }
        }

        m_AIGameEnemyEntity.m_possibleEntityTargets = possibleEntityTargets;
        m_AIGameEnemyEntity.m_possibleBuildingTargets = possibleBuildingTargets;
    }
}
