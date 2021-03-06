﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStep
{
    public AIGameEnemyUnit m_AIGameEnemyUnit;

    public AIStep(AIGameEnemyUnit AIGameEnemyUnit)
    {
        m_AIGameEnemyUnit = AIGameEnemyUnit;
    }

    public abstract IEnumerator TakeStepCoroutine();

    public abstract void TakeStepInstant();

    public virtual void CleanupAIStep() { }
}
