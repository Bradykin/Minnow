using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackOnceRepeatStep : AIAttackOnceStandardStep
{
    public AIAttackOnceRepeatStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) 
    {
        repeatAI = true;
    }

}
