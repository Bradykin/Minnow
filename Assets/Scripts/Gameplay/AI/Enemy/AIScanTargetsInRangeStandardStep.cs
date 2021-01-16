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
            if (tile.IsOccupied() && !tile.GetOccupyingUnit().m_isDead && m_AIGameEnemyUnit.m_gameEnemyUnit.CanHitUnit(tile.GetOccupyingUnit(), false))
            {
                if (tile.GetOccupyingUnit().GetFadeKeyword() != null)
                {
                    continue;
                }
                
                int damageAmountPerHit = tile.GetOccupyingUnit().CalculateDamageAmount(m_AIGameEnemyUnit.m_gameEnemyUnit.GetAttack(), DamageType.Unit);
                if (damageAmountPerHit == 0 && ignoreTargetsCantDamage && tile.GetOccupyingUnit().GetTauntKeyword() == null)
                {
                    continue;
                }

                possibleUnitTargets.Add(tile.GetOccupyingUnit());

                if (tile.GetOccupyingUnit().GetTauntKeyword() != null)
                {
                    m_AIGameEnemyUnit.m_tauntUnitTargets.Add(tile.GetOccupyingUnit());
                }

                //Rough code - goal is to determine if the enemy could kill the target in two hits
                int numHitsToRateVulnerable = 2;
                if (tile.GetOccupyingUnit().GetDamageShieldKeyword() != null)
                {
                    numHitsToRateVulnerable -= 1;
                }
                int damageAmountInVulnerableRange = 0;
                while (numHitsToRateVulnerable > 0)
                {
                    damageAmountInVulnerableRange += damageAmountPerHit;
                    numHitsToRateVulnerable--;
                }
                if (damageAmountInVulnerableRange >= tile.GetOccupyingUnit().GetCurHealth())
                {
                    m_AIGameEnemyUnit.m_vulnerableUnitTargets.Add(tile.GetOccupyingUnit());
                }
            }

            if (tile.HasBuilding() && !tile.GetBuilding().m_isDestroyed && tile.GetBuilding().GetTeam() == Team.Player && !tile.IsOccupied())
            {
                possibleBuildingTargets.Add(tile.GetBuilding());

                int numHitsToRateVulnerable = 2;
                int damageAmountInVulnerableRange = 0;
                while (numHitsToRateVulnerable > 0)
                {
                    damageAmountInVulnerableRange += m_AIGameEnemyUnit.m_gameEnemyUnit.GetAttack();
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
