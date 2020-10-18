using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameActionIntermission : GameElementBase
{
    public int m_actionCost = 1;

    public abstract void Activate();
    
    public virtual void SpendCost()
    {
        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        player.SpendActions(m_actionCost);
    }

    public string GetDesc()
    {
        return m_desc;
    }

    public virtual bool CanAfford()
    {
        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return false;
        }

        if (player.GetCurActions() < m_actionCost)
        {
            return false;
        }

        return true;
    }
}

