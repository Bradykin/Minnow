using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVictoriousKeyword : GameKeywordBase
{
    private GameAction m_action;

    public GameVictoriousKeyword(GameAction action)
    {
        m_action = action;

        m_name = "Victorious";
        m_desc = "When this entity kills another entity: " + action.m_desc;
    }

    public void DoAction()
    {
        m_action.DoAction();
    }
}
