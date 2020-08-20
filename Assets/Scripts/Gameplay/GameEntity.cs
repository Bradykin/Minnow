using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Player,
    Enemy
}

public abstract class GameEntity : GameElementBase
{
    //General data.  This should be set for every entity
    protected Team m_team;
    protected int m_curHealth;
    protected int m_maxHealth;
    protected int m_curAP;
    protected int m_apRegen;
    protected int m_maxAP;
    protected int m_power;

    //Specific data.  Only set if it varies from the default.  Be sure to add to the description so it shows up in the UI.
    protected int m_range = 1;
    protected int m_apToAttack = 2;

    //Functionality
    public GameTile m_curTile;
    public bool m_isDead;

    protected virtual void LateInit()
    {
        m_curHealth = m_maxHealth;
        m_curAP = m_maxAP;
    }

    public virtual int Hit(int damage)
    {
        if (damage <= 0)
        {
            damage = 1; //Always deal at least 1 damage
        }

        m_curHealth -= damage;

        if (m_curHealth <= 0)
        {
            Die();
        }

        return damage;
    }

    public virtual void Die()
    {
        m_isDead = true;
    }

    public virtual void Heal(int toHeal)
    {
        m_curHealth += toHeal;

        if (m_curHealth >= m_maxHealth)
        {
            m_curHealth = m_maxHealth;
        }
    }

    public virtual bool CanHitEntity(GameEntity other)
    {
        if (!IsInRangeOfEntity(other))
        {
            return false;
        }

        if (!HasAPToAttack())
        {
            return false;
        }

        if (GetTeam() == other.GetTeam()) //Can't attack your own team
        {
            return false;
        }

        return true;
    }

    public virtual bool IsInRangeOfEntity(GameEntity other)
    {
        int distance = WorldGridManager.Instance.CalculateAStarPath(m_curTile, other.m_curTile).Count;

        if ((distance - 1) > m_range)
        {
            return false;
        }

        return true;
    }

    public virtual bool HasAPToAttack()
    {
        if (m_curAP < m_apToAttack)
        {
            return false;
        }

        return true;
    }

    public virtual int HitEntity(GameEntity other)
    {
        m_curAP -= m_apToAttack;
        int damageTaken = other.Hit(GetPower());

        return damageTaken;
    }

    public Team GetTeam()
    {
        return m_team;
    }

    public int GetCurAP()
    {
        return m_curAP;
    }

    public int GetRange()
    {
        return m_range;
    }

    public int GetPower()
    {
        return m_power;
    }

    public override UITooltipController InitTooltip()
    {
        UITooltipController tooltipController = base.InitTooltip();
        tooltipController.m_titleBackground.color = GetColor();

        string healthString = "Health: " + m_curHealth + "/" + m_maxHealth;
        string powerString = "Power: " + m_power;
        string apString = "AP: " + m_curAP + "/" + m_maxAP + "(+" + m_apRegen + "/turn)";
        tooltipController.m_descText.text += "\n" + healthString + "\n" + powerString + "\n" + apString;

        return tooltipController;
    }

    public override Color GetColor()
    {
        if (GetTeam() == Team.Player)
        {
            return UIHelper.m_playerColor;
        }
        else if (GetTeam() == Team.Enemy)
        {
            return UIHelper.m_enemyColor;
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
        SpendAP(WorldGridManager.Instance.GetPathLength(m_curTile, tile));

        m_curTile.ClearEntity();
        tile.PlaceEntity(this);
    }

    public void SpendAP(int toSpend)
    {
        m_curAP -= toSpend;
    }
}
