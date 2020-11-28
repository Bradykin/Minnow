using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApplyKeywordToOtherOnMomentumAction : GameAction
{
    private GameUnit m_unit;
    private GameKeywordBase m_keyword;

    public GameApplyKeywordToOtherOnMomentumAction(GameUnit unit, GameKeywordBase keyword)
    {
        m_unit = unit;
        m_keyword = keyword;

        m_name = "Apply Keyword To Other On Momentum";
        m_actionParamType = ActionParamType.UnitKeywordParam;
    }

    public override string GetDesc()
    {
        return $"Add {m_keyword.GetDesc()} {m_keyword.GetName()} to the target unit.";
    }

    public override void DoAction()
    {
        Debug.Log("Called wrong DoAction in GameApplyKeywordToOtherOnMomentum - Use DoAction(GameUnit targetUnit).");
    }

    public void DoAction(GameUnit targetUnit)
    {
        if (targetUnit != null && !targetUnit.m_isDead)
        {
            targetUnit.AddKeyword(m_keyword, false, true);
        }
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
