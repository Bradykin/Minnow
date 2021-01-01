using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnrageKeyword : GameActionKeywordBase
{
    public GameEnrageKeyword(GameAction action)
    {
        m_actions.Add(action);

        m_name = "Enrage";
        m_focusInfoText = "Triggers when this unit takes damage.";
        m_shortDesc = "On take damage";
        m_keywordParamType = KeywordParamType.ActionParam;
    }
}
