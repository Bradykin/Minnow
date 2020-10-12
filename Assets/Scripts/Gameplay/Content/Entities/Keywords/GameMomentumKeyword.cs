using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMomentumKeyword : GameKeywordBase
{
    private GameAction m_action;

    public GameMomentumKeyword(GameAction action)
    {
        m_action = action;

        m_name = "Momentum";
        m_focusInfoText = "Triggers when this unit hits another unit.";
        m_shortDesc = "On hit";
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
