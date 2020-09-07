﻿using System.Collections;
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
        m_action = GameActionFactory.GetActionWithName(jsonData.actionName);
    }
}
