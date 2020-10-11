using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyUnit : GameUnit
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

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.EnemyHealth))
        {
            m_maxHealth = Mathf.FloorToInt(m_maxHealth * 1.5f);
        }

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.EnemyPower))
        {
            m_power = Mathf.FloorToInt(m_power * 1.5f);
        }

        m_curHealth = GetMaxHealth();
        m_curStamina = GetMaxStamina(); //Enemy units start at max Stamina.  This helps them get to the player base faster.

        m_typeline = Typeline.Monster;
    }

    //============================================================================================================//

    public override void Die()
    {
        base.Die();
        m_gameOpponentController.m_controlledUnits.Remove(this);
    }
}
