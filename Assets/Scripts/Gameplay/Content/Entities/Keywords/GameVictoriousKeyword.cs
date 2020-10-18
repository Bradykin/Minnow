using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVictoriousKeyword : GameActionKeywordBase
{
    public GameVictoriousKeyword(GameAction action)
    {
        m_actions.Add(action);

        m_name = "Victorious";
        m_focusInfoText = "Triggers when this unit kills another unit.";
        m_shortDesc = "On kill";
        m_keywordParamType = KeywordParamType.ActionParam;
    }
}
