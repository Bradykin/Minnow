using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackUntilOutOfAPStep : AIStep
{
    public AIAttackUntilOutOfAPStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override void TakeStep()
    {
        if (m_AIGameEnemyEntity.m_targetGameElement == null || !m_AIGameEnemyEntity.m_gameEnemyEntity.IsInRangeOfGameElement(m_AIGameEnemyEntity.m_targetGameElement))
        {
            return;
        }

        while(m_AIGameEnemyEntity.m_gameEnemyEntity.HasAPToAttack())
        {
            bool didAttack = false;
            switch (m_AIGameEnemyEntity.m_targetGameElement)
            {
                case GameEntity gameEntity:
                    didAttack = true;
                    m_AIGameEnemyEntity.m_gameEnemyEntity.HitEntity(gameEntity);
                    break;
                case GameBuildingBase gameBuildingBase:
                    didAttack = true;
                    m_AIGameEnemyEntity.m_gameEnemyEntity.HitBuilding(gameBuildingBase);
                    break;
            }

            if (!didAttack)
                break;
        }
    }
}
