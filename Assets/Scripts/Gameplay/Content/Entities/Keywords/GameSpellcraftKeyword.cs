using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpellcraftKeyword : GameKeywordBase
{
    private GameAction m_action;

    public GameSpellcraftKeyword(GameAction action)
    {
        m_action = action;

        m_name = "Spellcraft";
        m_desc = "When a spell is cast: " + action.m_desc;
    }

    public void DoAction()
    {
        m_action.DoAction();
    }
}
