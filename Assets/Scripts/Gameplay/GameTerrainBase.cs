using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameTerrainBase : GameElementBase
{
    public int m_damageReduction { get; protected set; }

    protected bool m_isPassable = true;
    protected int m_costToPass;

    //Only call these from the GameTile.  If you want these from outside, grab them from the GameTile functions instead of here.
    public bool IsPassable()
    {
        return m_isPassable;
    }

    public int GetCostToPass()
    {
        return m_costToPass;
    }
}
