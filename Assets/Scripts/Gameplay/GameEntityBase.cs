using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Player,
    Enemy
}

public abstract class GameEntityBase : GameElementBase
{
    protected Team m_team;

    protected int m_curHealth = 10;
    protected int m_maxHealth = 10;
    protected int m_curAP = 0;
    protected int m_apRegen = 3;
    protected int m_maxAP = 6;
    protected int m_power = 3;
    protected int m_toughness = 1;

    protected virtual void LateInit()
    {
        m_curHealth = m_maxHealth;
        m_curAP = m_maxAP;
    }

    public virtual void Hit(int damage)
    {
        int damageToDeal = damage - GetToughness();

        if (damageToDeal <= 0)
        {
            damageToDeal = 1; //Always deal at least 1 damage
        }

        m_curHealth -= damageToDeal;

        if (m_curHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        EngineLog.LogInfo(m_name + " has died!");
    }

    public virtual void Heal(int toHeal)
    {
        m_curHealth += toHeal;

        if (m_curHealth >= m_maxHealth)
        {
            m_curHealth = m_maxHealth;
        }
    }

    public Team GetTeam()
    {
        return m_team;
    }

    public int GetToughness()
    {
        return m_toughness;
    }

    public override void InitTooltip(UITooltipController tooltipController)
    {
        base.InitTooltip(tooltipController);

        if (GetTeam() == Team.Player)
        {
            tooltipController.m_titleBackground.color = Color.blue;
        }
        else if (GetTeam() == Team.Enemy)
        {
            tooltipController.m_titleBackground.color = Color.red;
        }
    }
}
