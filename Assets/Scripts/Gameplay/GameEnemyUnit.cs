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

    public int m_experienceAmount = 5;

    public GameEnemyUnit(GameOpponent gameOpponent)
    {
        m_AIGameEnemyUnit = new AIGameEnemyUnit(this);
        m_gameOpponentController = gameOpponent;
    }

    protected override void LateInit()
    {
        base.LateInit();

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.EnemyStrength))
        {
            m_maxHealth = Mathf.FloorToInt(m_maxHealth * 1.5f);
        }

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.EnemyStrength))
        {
            m_power = Mathf.FloorToInt(m_power * 1.5f);
        }

        SetHealthStaminaValues();

        m_typeline = Typeline.Monster;
    }

    //============================================================================================================//

    public override void Die()
    {
        GameHelper.GetGameController().AddPlaythroughExperience(m_experienceAmount);

        base.Die();
        m_gameOpponentController.m_controlledUnits.Remove(this);
    }
}
