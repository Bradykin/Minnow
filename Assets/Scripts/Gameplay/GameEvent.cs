using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : GameElementBase
{
    public GameTile m_tile;
    public int m_APCost;
    public string m_eventDesc;

    public bool m_isComplete;

    public GameEventOption m_optionOne;
    public GameEventOption m_optionTwo;
    public GameEventOption m_optionThree;

    protected virtual void LateInit()
    {
        m_APCost = 2;
    }

    public virtual bool isValidToSpawn(GameTile tile)
    {
        if (tile.m_building != null)
        {
            return false;
        }

        return true;
    }
}
