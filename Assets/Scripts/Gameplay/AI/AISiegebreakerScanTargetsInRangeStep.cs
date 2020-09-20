﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISiegebreakerScanTargetsInRangeStep : AIStep
{
    private int m_scanRadius = 4;
    
    public AISiegebreakerScanTargetsInRangeStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }
    
    public override void TakeStep()
    {
        List<GameTile> tilesInScanRange = WorldGridManager.Instance.GetSurroundingTiles(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), m_scanRadius);

        if (tilesInScanRange == null)
            return;

        List<GameEntity> possibleEntityTargets = new List<GameEntity>();
        List<GameBuildingBase> possibleBuildingTargets = new List<GameBuildingBase>();

        foreach (var tile in tilesInScanRange)
        {
            if (tile.HasBuilding() && !tile.GetBuilding().m_isDestroyed)
            {
                possibleBuildingTargets.Add(tile.GetBuilding());

                int numHitsToRateVulnerable = 2;
                int damageAmountInVulnerableRange = 0;
                while (numHitsToRateVulnerable > 0)
                {
                    damageAmountInVulnerableRange += m_AIGameEnemyEntity.m_gameEnemyEntity.GetPower();
                    numHitsToRateVulnerable--;
                }
                if (damageAmountInVulnerableRange >= tile.GetBuilding().GetCurHealth())
                {
                    m_AIGameEnemyEntity.m_vulnerableBuildingTargets.Add(tile.GetBuilding());
                }
            }
        }

        m_AIGameEnemyEntity.m_possibleEntityTargets = possibleEntityTargets;
        m_AIGameEnemyEntity.m_possibleBuildingTargets = possibleBuildingTargets;
    }
}
