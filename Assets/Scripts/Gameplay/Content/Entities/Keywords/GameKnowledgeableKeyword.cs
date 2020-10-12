using Newtonsoft.Json;
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
        m_focusInfoText = "Triggers when the player draws an extra card.";
        m_shortDesc = "On draw an extra card.";
        m_keywordParamType = KeywordParamType.ActionParam;

        if (action == null)
        {
            return;
        }

        m_desc = action.m_desc;
    }

    public void DoAction()
    {
        m_action.DoAction();
    }

    public override string SaveToJsonAsString()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            actionJson = m_action.SaveToJson()
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
