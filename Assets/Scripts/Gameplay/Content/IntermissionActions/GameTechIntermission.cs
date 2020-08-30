using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameTechIntermission : GameElementBase
{
    public int m_actionCost = 1;
    public GameWallet m_cost { get; protected set; }

    public abstract void Activate();

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

        if (!player.m_wallet.CanAfford(m_cost))
        {
            return false;
        }

        return true;
    }
}