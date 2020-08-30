using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBuildingIntermission
{
    public int m_actionCost = 1;
    public GameWallet m_cost { get; protected set; }

    public GameBuildingBase m_building;

    public void Init(GameBuildingBase building)
    {
        m_building = building;

        m_cost = new GameWallet(10, 5, 15);
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

        if (!player.m_wallet.CanAfford(m_cost))
        {
            return false;
        }

        return true;
    }
}