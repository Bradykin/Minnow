using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AIMoveStep : AIStep
{
    public AIMoveStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    protected void MoveTowardsCastle(int amountStaminaToSpend)
    {
        if (GameHelper.GetPlayer() != null && GameHelper.GetPlayer().Castle != null)
        {
            m_AIGameEnemyUnit.m_gameEnemyUnit.MoveTowards(GameHelper.GetPlayer().Castle.GetGameTile(), amountStaminaToSpend);
        }
    }
}