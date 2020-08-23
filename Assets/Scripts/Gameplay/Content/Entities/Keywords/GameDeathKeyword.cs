using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDeathKeyword : GameKeywordBase
{
    private GameAction m_action;

    public GameDeathKeyword(GameAction action)
    {
        m_action = action;

        m_name = "Death";
        m_desc = "When this entity dies: " + action.m_desc;
    }

    public void DoAction()
    {
        m_action.DoAction();
    }
}
