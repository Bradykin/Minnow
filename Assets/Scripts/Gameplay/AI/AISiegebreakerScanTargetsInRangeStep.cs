using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.XR.WSA;

public class AISiegebreakerScanTargetsInRangeStep : AIStep
{
    private int m_scanRadius = 4;
    
    public AISiegebreakerScanTargetsInRangeStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }
    
    public override IEnumerator TakeStep(bool yield)
    {
        List<GameTile> tilesInScanRange = WorldGridManager.Instance.GetSurroundingTiles(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), m_scanRadius);

        if (tilesInScanRange == null)
        {
            yield break;
        }

        for (int i = 0; i < tilesInScanRange.Count; i++)
        {
            m_AIGameEnemyUnit.m_newAIDebugLog.m_tilesScannedForTargets.Add(tilesInScanRange[i].m_gridPosition.ToString());
        }

        List<GameUnit> possibleUnitTargets = new List<GameUnit>();
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
