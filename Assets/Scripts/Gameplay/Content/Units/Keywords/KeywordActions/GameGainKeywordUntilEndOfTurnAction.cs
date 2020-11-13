using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainKeywordUntilEndOfTurnAction : GameAction
{
    private GameUnit m_unit;
    private GameKeywordBase m_keyword;

    public GameGainKeywordUntilEndOfTurnAction(GameUnit unit, GameKeywordBase keyword)
    {
        m_unit = unit;
        m_keyword = keyword;

        m_name = "Gain Keyword Until End Of Turn";
        m_actionParamType = ActionParamType.UnitKeywordParam;
    }

    public override string GetDesc()
    {
        return $"Add {m_keyword.GetDesc()} {m_keyword.GetName()} to {m_unit.GetName()}";
    }

    public override void DoAction()
    {
        GameKeywordBase tempKeyword = GameKeywordFactory.GetKeywordClone(m_keyword, m_unit);
        m_unit.AddKeyword(m_keyword);
        GameHelper.GetPlayer().AddScheduledAction(ScheduledActionTime.EndOfTurn, new GameLoseKeywordAction(m_unit, tempKeyword));
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

    public override JsonGameActionData SaveToJson()
    {
        JsonGameActionData jsonData = new JsonGameActionData
        {
            name = m_name,
            gameKeywordData = m_keyword.SaveToJson()
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
