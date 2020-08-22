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
}
