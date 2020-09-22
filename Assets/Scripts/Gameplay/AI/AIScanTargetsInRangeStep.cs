using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScanTargetsInRangeStep : AIStep
{
    public AIScanTargetsInRangeStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }
    
    public override void TakeStep()
    {
        List<GameTile> tilesInAttackRange = WorldGridManager.Instance.GetTilesInAttackRange(m_AIGameEnemyEntity.m_gameEnemyEntity.GetGameTile(), false, false);

        if (tilesInAttackRange == null)
            return;

        List<GameEntity> possibleEntityTargets = new List<GameEntity>();
        List<GameBuildingBase> possibleBuildingTargets = new List<GameBuildingBase>();

        foreach (var tile in tilesInAttackRange)
        {
            if (tile.IsOccupied() && !tile.m_occupyingEntity.m_isDead && tile.m_occupyingEntity.GetTeam() == Team.Player)
            {
                int damageAmountPerHit = tile.m_occupyingEntity.CalculateDamageAmount(m_AIGameEnemyEntity.m_gameEnemyEntity.GetPower());
                if (damageAmountPerHit == 0)
                {
                    continue;
                }

                possibleEntityTargets.Add(tile.m_occupyingEntity);

                //Rough code - goal is to determine if the enemy could kill the target in two hits
                int numHitsToRateVulnerable = 2;
                if (tile.m_occupyingEntity.GetKeyword<GameDamageShieldKeyword>() != null)
                {
                    numHitsToRateVulnerable -= tile.m_occupyingEntity.GetKeyword<GameDamageShieldKeyword>().m_numShields;
                }
                int damageAmountInVulnerableRange = 0;
                while (numHitsToRateVulnerable > 0)
                {
                    damageAmountInVulnerableRange += damageAmountPerHit;
                    numHitsToRateVulnerable--;
                }
                if (damageAmountInVulnerableRange >= tile.m_occupyingEntity.GetCurHealth())
                {
                    m_AIGameEnemyEntity.m_vulnerableEntityTargets.Add(tile.m_occupyingEntity);
                }
            }

            if (tile.HasBuilding() && !tile.GetBuilding().m_isDestroyed && tile.GetBuilding().m_buildingType != BuildingType.Defensive)
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
