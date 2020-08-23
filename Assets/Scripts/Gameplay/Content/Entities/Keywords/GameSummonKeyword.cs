using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSummonKeyword : GameKeywordBase
{
    private GameAction m_action;

    public GameSummonKeyword(GameAction action)
    {
        m_action = action;

        m_name = "Summon";
        m_desc = "When this entity gets summoned: " + action.m_desc;
    }

    public void DoAction()
    {
        m_action.DoAction();
    }
}
