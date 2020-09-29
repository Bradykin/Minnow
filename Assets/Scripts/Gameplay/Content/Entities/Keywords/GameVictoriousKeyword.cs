using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVictoriousKeyword : GameKeywordBase
{
    private GameAction m_action;

    public GameVictoriousKeyword(GameAction action)
    {
        m_action = action;

        m_name = "Victorious";
        m_focusInfoText = "Triggers when this unit kills another unit.";
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

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
