using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AILavaRhinoMoveToTargetStep : AIMoveToTargetStandardStep
{
    public AILavaRhinoMoveToTargetStep(AIGameEnemyUnit AIGameEnemyUnit, int numTurnsDelayMovement = 0) : base(AIGameEnemyUnit, numTurnsDelayMovement) 
    {
        letPassEnemies = true;
    }

    protected override int GetStaminaToUseToMoveToCastle()
    {
        return m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina();
    }
}