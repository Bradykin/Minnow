using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBlindBeastScanTargetsInRangeStep : AIScanTargetsInRangeStandardStep
{
    public AIBlindBeastScanTargetsInRangeStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    protected override List<GameTile> GetTilesToScan()
    {
        return WorldGridManager.Instance.GetSurroundingGameTiles(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), 1);
    }
}
