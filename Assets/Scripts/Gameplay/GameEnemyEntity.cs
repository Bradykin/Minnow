using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyEntity : GameEntity, ITakeTurnAI
{
    public AIGameEnemyEntity m_AIGameEnemyEntity;

    public GameEnemyEntity()
    {
        m_AIGameEnemyEntity = new AIGameEnemyEntity(this);
    }

    //============================================================================================================//

    public void TakeTurn()
    {
        m_AIGameEnemyEntity.TakeTurn();
    }
}
