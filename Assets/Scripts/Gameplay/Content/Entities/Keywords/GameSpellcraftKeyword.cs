﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpellcraftKeyword : GameKeywordBase
{
    private GameAction m_action;

    public GameSpellcraftKeyword(GameAction action)
    {
        m_action = action;

        m_name = "Spellcraft";
        m_focusInfoText = "Triggers when the player casts a spell card.";
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
