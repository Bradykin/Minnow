using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMoveToTileStandardStep : AIMoveStep
{
    public AIMoveToTileStandardStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override void TakeStep()
    {
        GameTile targetTile = m_AIGameEnemyUnit.m_targetGameTile;

        if (targetTile == null)
        {
            MoveTowardsCastle(m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen());
            return;
        }

        m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(targetTile);
    }
}