using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainRangeAction : GameAction
{
    private GameUnit m_unit;
    private int m_toGain;

    public GameGainRangeAction(GameUnit unit, int toGain)
    {
        m_unit = unit;
        m_toGain = toGain;

        m_name = "Gain Range";
        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override string GetDesc()
    {
        return "+ " + m_toGain + " range";
    }

    public override void DoAction()
    {
        m_unit.AddKeyword(new GameRangeKeyword(m_toGain));
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainRangeAction tempAction = (GameGainRangeAction)toAdd;

        m_toGain += tempAction.m_toGain;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainRangeAction tempAction = (GameGainRangeAction)toSubtract;

        m_toGain -= tempAction.m_toGain;
    }

    public override bool ShouldBeRemoved()
    {
        return m_toGain <= 0;
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
            intValue1 = m_toGain
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}