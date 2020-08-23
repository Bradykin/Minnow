using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMomentumKeyword : GameKeywordBase
{
    private GameAction m_action;

    public GameMomentumKeyword(GameAction action)
    {
        m_action = action;

        m_name = "Momentum";
        m_desc = "When this entity hits an entity: " + action.m_desc;
    }

    public void DoAction()
    {
        m_action.DoAction();
    }
}
