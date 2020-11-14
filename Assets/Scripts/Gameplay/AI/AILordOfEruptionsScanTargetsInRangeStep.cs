using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILordOfEruptionsScanTargetsInRangeStep : AIScanTargetsInRangeStandardStep
{
    private ContentLordOfEruptionsEnemy lordOfEruptionsEnemy;

    public AILordOfEruptionsScanTargetsInRangeStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) 
    {
        if (!(m_AIGameEnemyUnit.m_gameEnemyUnit is ContentLordOfEruptionsEnemy))
        {
            Debug.LogError("Wrong unit using Lord of Eruptions AI script.");
        }

        lordOfEruptionsEnemy = (ContentLordOfEruptionsEnemy)m_AIGameEnemyUnit.m_gameEnemyUnit;
    }

    protected override List<GameTile> GetTilesToScan()
    {
        return WorldGridManager.Instance.GetSurroundingGameTiles(lordOfEruptionsEnemy.GetGameTile(), lordOfEruptionsEnemy.GetRange() + lordOfEruptionsEnemy.m_teleportRange);
    }
}
