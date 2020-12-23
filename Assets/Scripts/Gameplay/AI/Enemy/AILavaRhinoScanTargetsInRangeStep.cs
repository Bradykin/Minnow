using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.XR.WSA;

public class AILavaRhinoScanTargetsInRangeStep : AIScanTargetsInRangeStandardStep
{
    public AILavaRhinoScanTargetsInRangeStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }
    
    public override void TakeStepInstant()
    {
        List<GameTile> tilesInMoveRange = GetTilesToScan();

        if (tilesInMoveRange == null)
        {
            return;
        }

        List<GameBuildingBase> possibleBuildingTargets = new List<GameBuildingBase>();

        foreach (var tile in tilesInMoveRange)
        {
            if (tile.HasBuilding() && !tile.GetBuilding().m_isDestroyed && tile.GetBuilding().GetTeam() == Team.Player)
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

        m_AIGameEnemyUnit.m_possibleUnitTargets = new List<GameUnit>();
        m_AIGameEnemyUnit.m_possibleBuildingTargets = possibleBuildingTargets;
    }

    protected override List<GameTile> GetTilesToScan()
    {
        return WorldGridManager.Instance.GetTilesInMovementRange(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), false, false);
    }
}
