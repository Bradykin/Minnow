﻿using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyEntity : GameUnit, ITakeTurnAI
{
    public AIGameEnemyUnit m_AIGameEnemyEntity;
    public GameOpponent m_gameOpponentController;

    public bool m_isElite;
    public bool m_isBoss;

    public int m_minWave;
    public int m_maxWave;

    public GameEnemyEntity(GameOpponent gameOpponent)
    {
        m_AIGameEnemyEntity = new AIGameEnemyUnit(this);
        m_gameOpponentController = gameOpponent;
    }

    protected override void LateInit()
    {
        base.LateInit();

        if (GameHelper.IsValidChaosLevel(7))
        {
            m_maxHealth = m_maxHealth * 2;
        }

        if (GameHelper.IsValidChaosLevel(8))
        {
            m_power = m_power * 2;
        }

        m_curHealth = GetMaxHealth();
        m_curStamina = GetMaxStamina(); //Enemy entities start at max Stamina.  This helps them get to the player base faster.

        m_typeline = Typeline.Monster;
    }

    //============================================================================================================//

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
