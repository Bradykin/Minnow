using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyUnit : GameUnit, ITakeTurnAI
{
    public AIGameEnemyUnit m_AIGameEnemyUnit;
    public GameOpponent m_gameOpponentController;

    public bool m_isElite;
    public bool m_isBoss;

    public int m_minWave;
    public int m_maxWave;

    public GameEnemyUnit(GameOpponent gameOpponent)
    {
        m_AIGameEnemyUnit = new AIGameEnemyUnit(this);
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
        m_curStamina = GetMaxStamina(); //Enemy units start at max Stamina.  This helps them get to the player base faster.

        m_typeline = Typeline.Monster;
    }

    //============================================================================================================//

    public IEnumerator TakeTurn(bool yield)
    {
        if (yield)
        {
            yield return FactoryManager.Instance.StartCoroutine(m_AIGameEnemyUnit.TakeTurn(yield));
        }
        else
        {
            FactoryManager.Instance.StartCoroutine(m_AIGameEnemyUnit.TakeTurn(yield));
        }
    }

    public override void Die()
    {
        base.Die();
        m_gameOpponentController.m_controlledUnits.Remove(this);
    }
}
