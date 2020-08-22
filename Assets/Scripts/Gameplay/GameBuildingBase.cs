using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBuildingBase : GameElementBase
{
    protected WorldTile m_tile;

    public int m_curHealth;
    public int m_maxHealth;

    public bool m_isDestroyed;

    public void SetWorldTile(WorldTile worldTile)
    {
        m_tile = worldTile;
    }

    public void LateInit()
    {
        m_icon = UIHelper.GetIconBuilding(m_name);
        m_curHealth = m_maxHealth;
    }

    public virtual Sprite GetIcon()
    {
        return m_icon;
    }

    public void Hit(int damage)
    {
        m_curHealth -= damage;

        if (m_curHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        m_isDestroyed = true;

        UIHelper.CreateWorldElementNotification(m_name + " has been destroyed by the battle!", false, m_tile);
    }

    public virtual void EndTurn() { }
}
