using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnrageKeyword : GameKeywordBase
{
    private GameAction m_action;

    public GameEnrageKeyword(GameAction action)
    {
        m_action = action;

        m_name = "Enrage";
        m_desc = "When this entity gets hit: " + action.m_desc;
        m_keywordParamType = KeywordParamType.ActionParam;
    }

    public void DoAction()
    {
        m_action.DoAction();
    }

    public override string SaveToJson()
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
