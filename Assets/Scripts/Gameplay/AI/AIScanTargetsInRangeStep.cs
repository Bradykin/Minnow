using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScanTargetsInRangeStep : AIStep
{
    protected bool ignoreTargetsCantDamage = true;

    public AIScanTargetsInRangeStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStep()
    {
        List<GameTile> tilesInAttackRange = WorldGridManager.Instance.GetTilesInRangeToMoveAndAttack(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), false, false);

        if (tilesInAttackRange == null)
        {
            yield break;
        }

        for (int i = 0; i < tilesInAttackRange.Count; i++)
        {
            m_AIGameEnemyUnit.m_newAIDebugLog.m_tilesScannedForTargets.Add(tilesInAttackRange[i].m_gridPosition.ToString());
        }

        List<GameUnit> possibleUnitTargets = new List<GameUnit>();
        List<GameBuildingBase> possibleBuildingTargets = new List<GameBuildingBase>();

        foreach (var tile in tilesInAttackRange)
        {
            if (tile.IsOccupied() && !tile.m_occupyingUnit.m_isDead && m_AIGameEnemyUnit.m_gameEnemyUnit.CanHitUnit(tile.m_occupyingUnit, false))
            {
                int damageAmountPerHit = tile.m_occupyingUnit.CalculateDamageAmount(m_AIGameEnemyUnit.m_gameEnemyUnit.GetPower());
                if (ignoreTargetsCantDamage)
                {
                    if (damageAmountPerHit == 0)
                    {
                        continue;
                    }
                }

                possibleUnitTargets.Add(tile.m_occupyingUnit);

                //Rough code - goal is to determine if the enemy could kill the target in two hits
                int numHitsToRateVulnerable = 2;
                if (tile.m_occupyingUnit.GetKeyword<GameDamageShieldKeyword>() != null)
                {
                    numHitsToRateVulnerable -= tile.m_occupyingUnit.GetKeyword<GameDamageShieldKeyword>().m_numShields;
                }
                int damageAmountInVulnerableRange = 0;
                while (numHitsToRateVulnerable > 0)
                {
                    damageAmountInVulnerableRange += damageAmountPerHit;
                    numHitsToRateVulnerable--;
                }
                if (damageAmountInVulnerableRange >= tile.m_occupyingUnit.GetCurHealth())
                {
                    m_AIGameEnemyUnit.m_vulnerableUnitTargets.Add(tile.m_occupyingUnit);
                }
            }

            if (tile.HasBuilding() && !tile.GetBuilding().m_isDestroyed && tile.GetBuilding().m_buildingType != BuildingType.Defensive && !tile.IsOccupied())
            {
                possibleBuildingTargets.Add(tile.GetBuilding());

                int numHitsToRateVulnerable = 2;
                int damageAmountInVulnerableRange = 0;
                while (numHitsToRateVulnerable > 0)
                {
                    damageAmountInVulnerableRange += m_AIGameEnemyUnit.m_gameEnemyUnit.GetPower();
                    numHitsToRateVulnerable--;
                }
                if (damageAmountInVulnerableRange >= tile.GetBuilding().GetCurHealth())
                {
                    m_AIGameEnemyUnit.m_vulnerableBuildingTargets.Add(tile.GetBuilding());
                }
            }
        }

        m_AIGameEnemyUnit.m_possibleUnitTargets = possibleUnitTargets;
        m_AIGameEnemyUnit.m_possibleBuildingTargets = possibleBuildingTargets;
    }
}
