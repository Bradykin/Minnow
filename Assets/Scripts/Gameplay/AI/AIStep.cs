using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStep
{
    public AIGameEnemyEntity m_AIGameEnemyEntity;

    public AIStep(AIGameEnemyEntity AIGameEnemyEntity)
    {
        m_AIGameEnemyEntity = AIGameEnemyEntity;
    }

    public abstract IEnumerator TakeStep();
}
