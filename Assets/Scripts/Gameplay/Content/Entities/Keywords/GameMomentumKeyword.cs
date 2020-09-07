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
        m_desc = "When this entity hits an entity: " + action.m_desc;
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
            actionName = m_action.m_name
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
