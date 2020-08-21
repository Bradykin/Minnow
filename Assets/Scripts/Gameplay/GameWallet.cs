using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWallet
{
    public int m_gold;
    public int m_magic;
    public int m_bricks;

    public GameWallet()
    {

    }

    public GameWallet(int gold, int magic, int bricks)
    {
        m_gold = gold;
        m_magic = magic;
        m_bricks = bricks;
    }

    public void AddResources(GameWallet toAdd)
    {
        m_gold += toAdd.m_gold;
        m_magic += toAdd.m_magic;
        m_bricks += toAdd.m_bricks;
    }

    public void SubtracatResources(GameWallet toAdd)
    {
        m_gold -= toAdd.m_gold;
        m_magic -= toAdd.m_magic;
        m_bricks -= toAdd.m_bricks;
    }

    public bool CanAfford(GameWallet cost)
    {
        if (CanAffordGold(cost) && CanAffordMagic(cost) && CanAffordBricks(cost))
        {
            return true;
        }

        return false;
    }

    private bool CanAffordGold(GameWallet cost)
    {
        return cost.m_gold <= m_gold;
    }

    private bool CanAffordMagic(GameWallet cost)
    {
        return cost.m_magic <= m_magic;
    }

    private bool CanAffordBricks(GameWallet cost)
    {
        return cost.m_bricks <= m_bricks;
    }
}
