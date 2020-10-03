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
            MoveTowardsCastle(m_AIGameEnemyEntity.m_gameEnemyEntity.GetStaminaRegen());
            return;
        }

        m_AIGameEnemyEntity.m_gameEnemyEntity.m_uiEntity.MoveTo(targetTile);
    }
}