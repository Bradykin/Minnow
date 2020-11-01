using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMomentumKeyword : GameActionKeywordBase
{
    public GameMomentumKeyword(GameAction action)
    {
        m_actions.Add(action);

        m_name = "Momentum";
        m_focusInfoText = "Triggers when this unit hits another unit or building.";
        m_shortDesc = "On hit";
        m_keywordParamType = KeywordParamType.ActionParam;
    }
}
