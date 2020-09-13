﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKnowledgeableKeyword : GameKeywordBase
{
    private GameAction m_action;

    public GameKnowledgeableKeyword(GameAction action)
    {
        m_action = action;

        m_name = "Knowledgable";
        m_desc = action.m_desc;
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
