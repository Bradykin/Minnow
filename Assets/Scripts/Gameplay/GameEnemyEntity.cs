using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyEntity : GameEntity, ITakeTurnAI
{
    public AIGameEnemyEntity m_AIGameEnemyEntity;
    public GameOpponent m_gameOpponentController;

    public GameEnemyEntity(GameOpponent gameOpponent)
    {
        m_AIGameEnemyEntity = new AIGameEnemyEntity(this);
        m_gameOpponentController = gameOpponent;
    }

    //============================================================================================================//

    public virtual bool IsAIAbleToAttack()
    {
        return HasAPToAttack();
    }

    public virtual void TakeTurn()
    {
        m_AIGameEnemyEntity.TakeTurn();
    }

    public override void Die()
    {
        base.Die();
        m_gameOpponentController.m_controlledEntities.Remove(this);
    }
}
