﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackUntilOutOfAPStandardStep : AIStep
{
    public AIAttackUntilOutOfAPStandardStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

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
                    if (gameEntity.m_isDead)
                    {
                        if (m_AIGameEnemyEntity.m_gameEnemyEntity.HasAPToAttack())
                        {
                            m_AIGameEnemyEntity.m_doSteps = true;
                        }
                        return;
                    }
                    break;
                case GameBuildingBase gameBuildingBase:
                    didAttack = true;
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

            if (!didAttack)
                break;
        }
    }
}