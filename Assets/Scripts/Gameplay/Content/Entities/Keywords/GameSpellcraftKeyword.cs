using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpellcraftKeyword : GameKeywordBase
{
    private GameAction m_action;

    public GameSpellcraftKeyword(GameAction action)
    {
        m_action = action;

        m_name = "Spellcraft";
        m_desc = "When a spell is cast: " + action.m_desc;
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
