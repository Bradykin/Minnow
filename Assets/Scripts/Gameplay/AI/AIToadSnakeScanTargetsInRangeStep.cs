using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIToadSnakeScanTargetsInRangeStep : AIScanTargetsInRangeStandardStep
{
    public AIToadSnakeScanTargetsInRangeStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) 
    {
        ignoreTargetsCantDamage = false;
    }
}
