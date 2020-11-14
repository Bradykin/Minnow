using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScanTargetsInRangeStandardStep : AIStep
{
    protected bool ignoreTargetsCantDamage = true;

    public AIScanTargetsInRangeStandardStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStepCoroutine()
    {
        TakeStepInstant();
        yield break;
    }

    public override void TakeStepInstant()
    {
        List<GameTile> tilesInAttackRange = GetTilesToScan();

        if (tilesInAttackRange == null)
        {
            return;
        }

        List<GameUnit> possibleUnitTargets = new List<GameUnit>();
        List<GameBuildingBase> possibleBuildingTargets = new List<GameBuildingBase>();

        foreach (var tile in tilesInAttackRange)
        {
            if (tile.IsOccupied() && !tile.m_occupyingUnit.m_isDead && m_AIGameEnemyUnit.m_gameEnemyUnit.CanHitUnit(tile.m_occupyingUnit, false))
            {
                int damageAmountPerHit = tile.m_occupyingUnit.CalculateDamageAmount(m_AIGameEnemyUnit.m_gameEnemyUnit.GetPower(), DamageType.Unit);
                if (damageAmountPerHit == 0 && ignoreTargetsCantDamage && tile.m_occupyingUnit.GetTauntKeyword() == null)
                {
                    continue;
                }

                possibleUnitTargets.Add(tile.m_occupyingUnit);

                if (tile.m_occupyingUnit.GetTauntKeyword() != null)
                {
                    m_AIGameEnemyUnit.m_tauntUnitTargets.Add(tile.m_occupyingUnit);
                }

                //Rough code - goal is to determine if the enemy could kill the target in two hits
                int numHitsToRateVulnerable = 2;
                if (tile.m_occupyingUnit.GetDamageShieldKeyword() != null)
                {
                    numHitsToRateVulnerable -= tile.m_occupyingUnit.GetDamageShieldKeyword().GetShieldLevel();
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

            if (tile.HasBuilding() && !tile.GetBuilding().m_isDestroyed && tile.GetBuilding().GetTeam() == Team.Player && !tile.IsOccupied())
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

    protected virtual List<GameTile> GetTilesToScan()
    {
        return WorldGridManager.Instance.GetTilesInRangeToMoveAndAttack(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), false, false);
    }
}
