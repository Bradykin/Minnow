using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSummonKeyword : GameActionKeywordBase
{
    public GameSummonKeyword(GameAction action)
    {
        m_actions.Add(action);

        m_name = "Summon";
        m_focusInfoText = "Triggers when this unit is summoned.";
        m_keywordParamType = KeywordParamType.ActionParam;
    }
}
