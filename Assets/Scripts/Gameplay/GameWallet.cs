using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWallet
{
    public int m_gold;

    public GameWallet(int gold)
    {
        m_gold = gold;
    }

    public void AddGold(int toAdd, bool showUINotification = true)
    {
        m_gold += toAdd;

        if (showUINotification)
        {
            UIHelper.CreateWalletWorldElementNotification(toAdd);
        }
    }

    public void SpendGold(int toSpend)
    {
        m_gold -= toSpend;
    }

    public override string ToString()
    {
        return m_gold + " Gold";
    }
}
