using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKnowledgeableKeyword : GameKeywordBase
{
    private GameAction m_action;

    public GameKnowledgeableKeyword(GameAction action)
    {
        m_action = action;

        m_name = "Knowledgable";
        m_desc = "When a card is drawn aside from the starting hand each turn: " + action.m_desc;
    }

    public void DoAction()
    {
        m_action.DoAction();
    }
}
