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

    public GameTile m_curTile;

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
        Debug.Log(m_name + " has died!");
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

    public int GetCurAP()
    {
        return m_curAP;
    }

    public override UITooltipController InitTooltip()
    {
        UITooltipController tooltipController = base.InitTooltip();
        tooltipController.m_titleBackground.color = GetColor();

        string apString = "AP: " + m_curAP + "//" + m_maxAP + "(" + m_apRegen + ")";
        tooltipController.m_descText.text += "\n" + apString;

        return tooltipController;
    }

    public override Color GetColor()
    {
        if (GetTeam() == Team.Player)
        {
            return Color.blue;
        }
        else if (GetTeam() == Team.Enemy)
        {
            return Color.red;
        }

        return Color.white;
    }

    public bool CanMoveTo(GameTile tile)
    {
        if (tile.IsOccupied())
        {
            return false;
        }

        if (WorldGridManager.Instance.GetPathLength(m_curTile, tile) > m_curAP)
        {
            return false;
        }
        
        return true;
    }

    public void MoveTo(GameTile tile)
    {
        m_curAP -= WorldGridManager.Instance.GetPathLength(m_curTile, tile);

        m_curTile.ClearEntity();
        tile.PlaceEntity(this);
    }
}
