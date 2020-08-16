using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameTerrainBase : GameElementBase
{
    public bool m_isPassable { get; protected set; } = true;
    public int m_costToPass { get; protected set; }
}
