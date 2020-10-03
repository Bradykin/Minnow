using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISiegebreakerAttackUntilOutOfStaminaStep : AIAttackUntilOutOfStaminaStandardStep
{
    public AISiegebreakerAttackUntilOutOfStaminaStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override void TakeStep()
    {
        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina() == m_AIGameEnemyUnit.m_gameEnemyUnit.GetMaxStamina())
        {
            base.TakeStep();
        }
        
        /*if (m_AIGameEnemyEntity.m_targetToAttack == null || !m_AIGameEnemyEntity.m_gameEnemyEntity.IsInRangeOfGameElement(m_AIGameEnemyEntity.m_targetToAttack))
        {
            return;
        }

        while(m_AIGameEnemyEntity.m_gameEnemyEntity.IsAIAbleToAttack())
        {
            bool didAttack = false;
            switch (m_AIGameEnemyEntity.m_targetToAttack)
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
        }*/
    }
}
