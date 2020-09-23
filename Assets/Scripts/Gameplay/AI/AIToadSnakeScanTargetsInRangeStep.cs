using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIToadSnakeScanTargetsInRangeStep : AIScanTargetsInRangeStep
{
    public AIToadSnakeScanTargetsInRangeStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) 
    {
        ignoreTargetsCantDamage = false;
    }
}
