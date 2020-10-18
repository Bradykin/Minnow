using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKnowledgeableKeyword : GameActionKeywordBase
{
    public GameKnowledgeableKeyword(GameAction action)
    {
        m_actions.Add(action);

        m_name = "Knowledgable";
        m_focusInfoText = "Triggers when the player draws an extra card.";
        m_shortDesc = "On draw an extra card.";
        m_keywordParamType = KeywordParamType.ActionParam;
    }
}
