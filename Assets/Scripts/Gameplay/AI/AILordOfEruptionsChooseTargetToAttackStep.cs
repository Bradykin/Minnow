using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AILordOfEruptionsChooseTargetToAttackStep : AIChooseTargetToAttackStandardStep
{
    public AILordOfEruptionsChooseTargetToAttackStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override void TakeStepInstant()
    {
        List<GameTile> unitSurroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), 1, 0);
        for (int i = 0; i < unitSurroundingTiles.Count; i++)
        {
            if (unitSurroundingTiles[i].GetTerrain() is ContentVolcanoInactiveTerrain)
            {
                //There is a volcano to erupt nearby, don't need to scan for targets to attack
                return;
            }
        }

        base.TakeStepInstant();
    }
}