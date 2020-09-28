using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackOnceStandardStep : AIStep
{
    public AIAttackOnceStandardStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override void TakeStep()
    {
        if (m_AIGameEnemyEntity.m_targetGameElement == null || !m_AIGameEnemyEntity.m_gameEnemyEntity.IsInRangeOfGameElement(m_AIGameEnemyEntity.m_targetGameElement))
        {
            return;
        }

        if (m_AIGameEnemyEntity.m_gameEnemyEntity.HasAPToAttack())
        {
            switch (m_AIGameEnemyEntity.m_targetGameElement)
            {
                case GameEntity gameEntity:
                    m_AIGameEnemyEntity.m_gameEnemyEntity.HitEntity(gameEntity);
                    if (gameEntity.m_isDead || gameEntity == null)
                    {
                        if (m_AIGameEnemyEntity.m_gameEnemyEntity.HasAPToAttack())
                        {
                            m_AIGameEnemyEntity.m_doSteps = true;
                        }
                        return;
                    }
                    break;
                case GameBuildingBase gameBuildingBase:
                    m_AIGameEnemyEntity.m_gameEnemyEntity.HitBuilding(gameBuildingBase);
                    if (gameBuildingBase.m_isDestroyed)
                    {
                        if (m_AIGameEnemyEntity.m_gameEnemyEntity.HasAPToAttack())
                        {
                            m_AIGameEnemyEntity.m_doSteps = true;
                        }
                        return;
                    }
                    break;
            }
        }
    }
}
