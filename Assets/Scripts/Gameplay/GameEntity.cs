﻿using Game.Util;
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
        m_curHealth = GetMaxHealth();
        m_curAP = GetMaxAP();

        m_icon = UIHelper.GetIconEntity(m_name);
    }

    public virtual int Hit(int damage)
    {
        if (damage <= 0)
        {
            damage = 1; //Always deal at least 1 damage
        }

        m_curHealth -= damage;

        GameEnrageKeyword enrageKeyword = m_keywordHolder.GetKeyword<GameEnrageKeyword>();
        if (enrageKeyword != null)
        {
            enrageKeyword.DoAction();
        }

        if (m_curHealth <= 0)
        {
            Die();
        }

        return damage;
    }

    public virtual void Die()
    {
        if (GetTeam() == Team.Enemy)
        {
            GamePlayer player = GameHelper.GetPlayer();
            if (player != null)
            {
                int numRelics = GameHelper.RelicCount<ContentMorlemainsSkullRelic>();
                if (numRelics > 0)
                {
                    player.AddEnergy(numRelics);
                }
            }
        }

        m_isDead = true;
    }

    public virtual void Heal(int toHeal)
    {
        m_curHealth += toHeal;

        if (m_curHealth >= GetMaxHealth())
        {
            m_curHealth = GetMaxHealth();
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
        m_curAP = GetMaxAP();
    }

    public void EmptyAP()
    {
        m_curAP = 0;
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
        int toReturn = m_power;

        if (GetTeam() == Team.Player)
        {
            toReturn += 1 * GameHelper.RelicCount<ContentWolvenFangRelic>();
        }

        return toReturn;
    }

    public int GetCurHealth()
    {
        return m_curHealth;
    }

    public int GetMaxHealth()
    {
        int toReturn = m_maxHealth;

        if (GetTeam() == Team.Player)
        {
            toReturn += 3 * GameHelper.RelicCount<ContentOrbOfHealthRelic>();
        }

        return toReturn;
    }

    public int GetMaxAP()
    {
        int toReturn = m_maxAP;

        if (GetTeam() == Team.Player)
        {
            toReturn += 1 * GameHelper.RelicCount<ContentHourglassOfSpeedRelic>();
        }

        return toReturn;
    }

    public int GetAPRegen()
    {
        int toReturn = m_apRegen;

        toReturn += 1 * GameHelper.RelicCount<ContentSecretSoupRelic>();

        return toReturn;
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
        m_curAP += GetAPRegen();

        if (m_curAP > GetMaxAP())
        {
            m_curAP = GetMaxAP();
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
