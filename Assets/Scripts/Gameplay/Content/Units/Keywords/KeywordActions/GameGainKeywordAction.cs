﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainKeywordAction : GameAction
{
    private GameUnit m_unit;
    private GameKeywordBase m_keyword;

    public GameGainKeywordAction(GameUnit unit, GameKeywordBase keyword)
    {
        m_unit = unit;
        m_keyword = keyword;

        m_name = "Lose Keyword";
        m_actionParamType = ActionParamType.UnitKeywordParam;
    }

    public override string GetDesc()
    {
        return $"Add {m_keyword.GetDesc()} {m_keyword.GetName()} to {m_unit.GetName()}";
    }

    public override void DoAction()
    {
        m_unit.AddKeyword(m_keyword);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainKeywordAction tempAction = (GameGainKeywordAction)toAdd;

        m_keyword.AddKeyword(tempAction.GetKeyword());
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainKeywordAction tempAction = (GameGainKeywordAction)toSubtract;

        m_keyword.SubtractKeyword(tempAction.GetKeyword());
    }

    public override bool ShouldBeRemoved()
    {
        return m_keyword.ShouldBeRemoved();
    }

    public GameKeywordBase GetKeyword()
    {
        return m_keyword;
    }

    public override GameUnit GetGameUnit()
    {
        return m_unit;
    }

    public override JsonActionData SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            keywordValue = m_keyword.SaveToJson()
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}