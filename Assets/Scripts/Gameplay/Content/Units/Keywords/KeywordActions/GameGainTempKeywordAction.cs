using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainTempKeywordAction : GameAction
{
    private GameUnit m_unit;
    private GameKeywordBase m_keyword;

    public GameGainTempKeywordAction(GameUnit unit, GameKeywordBase keyword)
    {
        m_unit = unit;
        m_keyword = keyword;

        m_name = "Gain Temporary Keyword";
        m_actionParamType = ActionParamType.UnitKeywordParam;
    }

    public override string GetDesc()
    {
        return $"Add {m_keyword.GetDesc()} {m_keyword.GetName()} to {m_unit.GetName()}";
    }

    public override void DoAction()
    {
        m_unit.AddKeyword(m_keyword, false, false);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainTempKeywordAction tempAction = (GameGainTempKeywordAction)toAdd;

        m_keyword.AddKeyword(tempAction.GetKeyword());
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainTempKeywordAction tempAction = (GameGainTempKeywordAction)toSubtract;

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

    public override JsonGameActionData SaveToJson()
    {
        JsonGameActionData jsonData = new JsonGameActionData
        {
            name = m_name,
            gameKeywordData = m_keyword.SaveToJson(),
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
