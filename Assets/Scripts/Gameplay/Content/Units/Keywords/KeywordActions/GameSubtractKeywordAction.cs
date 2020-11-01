using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSubtractKeywordAction : GameAction
{
    private GameUnit m_unit;
    private GameKeywordBase m_keyword;

    public GameSubtractKeywordAction(GameUnit unit, GameKeywordBase keyword)
    {
        m_unit = unit;
        m_keyword = keyword;

        m_name = "Subtract Keyword";
        m_actionParamType = ActionParamType.UnitKeywordParam;
    }

    public override string GetDesc()
    {
        return $"Remove {m_keyword.GetDesc()} from {m_unit.GetName()}";
    }

    public override void DoAction()
    {
        m_unit.SubtractKeyword(m_keyword);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameSubtractKeywordAction tempAction = (GameSubtractKeywordAction)toAdd;

        m_keyword.AddKeyword(tempAction.GetKeywordToSubtract());
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameSubtractKeywordAction tempAction = (GameSubtractKeywordAction)toSubtract;

        m_keyword.SubtractKeyword(tempAction.GetKeywordToSubtract());
    }

    public override bool ShouldBeRemoved()
    {
        return m_keyword.ShouldBeRemoved();
    }

    public GameKeywordBase GetKeywordToSubtract()
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
