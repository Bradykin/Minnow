using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Player,
    Enemy
}

public abstract class GameEntity : GameElementBase, ITurns
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
    protected GameKeywordHolder m_keywordHolder = new GameKeywordHolder();
    protected int m_apToAttack = 2;

    //Functionality
    public GameTile m_curTile;
    public bool m_isDead;
    public UIEntity m_uiEntity;

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

        if ((distance - 1) > GetRange())
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

    public void FillAP()
    {
        m_curAP = m_maxAP;
    }

    public GameKeywordHolder GetKeywordHolder()
    {
        return m_keywordHolder;
    }

    public int GetRange()
    {
        GameRangeKeyword rangeKeyword = m_keywordHolder.GetKeyword<GameRangeKeyword>();
        if (rangeKeyword != null)
        {
            return rangeKeyword.m_range;
        }

        return 1;
    }

    public int GetPower()
    {
        return m_power;
    }

    public int GetCurHealth()
    {
        return m_curHealth;
    }

    public int GetMaxHealth()
    {
        return m_maxHealth;
    }

    public int GetMaxAP()
    {
        return m_maxAP;
    }

    public int GetAPRegen()
    {
        return m_apRegen;
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

    public string GetDesc()
    {
        string descString = m_desc + "\n";

        return descString;
    }

    private void RegenAP()
    {
        m_curAP += m_apRegen;

        if (m_curAP > m_maxAP)
        {
            m_curAP = m_maxAP;
        }
    }


    //============================================================================================================//

    public virtual void StartTurn() { }

    public virtual void EndTurn()
    {
        RegenAP();

        GameRegenerateKeyword regenKeyword = m_keywordHolder.GetKeyword<GameRegenerateKeyword>();
        if (regenKeyword != null)
        {
            Heal(regenKeyword.m_regenVal);
            UIHelper.CreateWorldElementNotification(m_name + " regenerates " + regenKeyword.m_regenVal, true, m_uiEntity);
        }
    }
}
