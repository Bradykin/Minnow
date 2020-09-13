using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMoveToTileStandardStep : AIMoveStep
{
    public AIMoveToTileStandardStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override void TakeStep()
    {
        GameTile targetTile = m_AIGameEnemyEntity.m_targetGameTile;

        if (targetTile == null)
        {
            MoveTowardsCastle();
            return;
        }

        m_AIGameEnemyEntity.m_gameEnemyEntity.MoveTo(targetTile);
    }
}