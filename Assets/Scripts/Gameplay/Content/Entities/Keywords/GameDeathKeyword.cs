using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDeathKeyword : GameActionKeywordBase
{
    public GameDeathKeyword(GameAction action)
    {
        m_actions.Add(action);

        m_name = "Death";
        m_focusInfoText = "Triggers when this unit dies.";
        m_keywordParamType = KeywordParamType.ActionParam;
    }
}
