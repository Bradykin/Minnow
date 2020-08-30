using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWallet
{
    public int m_gold;

    public GameWallet()
    {

    }

    public GameWallet(int gold)
    {
        m_gold = gold;
    }

    public void AddResources(GameWallet toAdd)
    {
        m_gold += toAdd.m_gold;
    }

    public void SubtractResources(GameWallet toAdd)
    {
        m_gold -= toAdd.m_gold;
    }

    public bool CanAfford(GameWallet cost)
    {
        if (CanAffordGold(cost))
        {
            return true;
        }

        return false;
    }

    private bool CanAffordGold(GameWallet cost)
    {
        return cost.m_gold <= m_gold;
    }

    public override string ToString()
    {
        return m_gold + " Gold";
    }
}
