using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AIMoveStep : AIStep
{
    public AIMoveStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    protected void MoveTowardsCastle(int amountStaminaToSpend)
    {
        if (GameHelper.GetPlayer() != null && GameHelper.GetPlayer().Castle != null)
        {
            m_AIGameEnemyEntity.m_gameEnemyEntity.MoveTowards(GameHelper.GetPlayer().Castle.GetGameTile(), amountStaminaToSpend);
        }
    }
}