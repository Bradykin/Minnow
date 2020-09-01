using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBuildingBase : GameElementBase, ITurns, ITakeTurnAI
{
    public WorldTile m_curTile;

    public Sprite m_destroyedIcon;
    public int m_curHealth;
    public int m_maxHealth;

    public int m_sightRange = 2;

    public bool m_isDestroyed;
    public bool m_expandsPlaceRange = false;

    public void SetWorldTile(WorldTile worldTile)
    {
        m_curTile = worldTile;
    }

    public void LateInit()
    {
        m_icon = UIHelper.GetIconBuilding(m_name);
        m_destroyedIcon = UIHelper.GetIconBuilding(m_name + "D");
        m_curHealth = m_maxHealth;
    }

    public virtual Sprite GetIcon()
    {
        if (m_isDestroyed)
        {
            return m_destroyedIcon;
        }
        else
        {
            return m_icon;
        }
    }

    public virtual int GetHit(int damage)
    {
        m_curHealth -= damage;

        UIHelper.CreateWorldElementNotification(m_name + " was hit for " + damage + " damage!", false, m_curTile);

        if (m_curHealth <= 0)
        {
            Die();
        }

        return damage;
    }

    protected virtual void Die()
    {
        m_isDestroyed = true;

        UIHelper.CreateWorldElementNotification(m_name + " has been destroyed by the battle!", false, m_curTile);
    }

    public abstract bool IsValidTerrainToPlace(GameTerrainBase terrain);

    public virtual void TriggerEndOfWave()
    {
        m_isDestroyed = false;
    }

    //============================================================================================================//

    public virtual void TakeTurn() { }

    //============================================================================================================//

    public virtual void StartTurn() { }

    public virtual void EndTurn() { }
}
