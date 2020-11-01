using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainBrittleAction : GameAction
{
    private GameUnit m_unit;
    private int m_brittleAmount;

    public GameGainBrittleAction(GameUnit unit, int brittleAmount)
    {
        m_unit = unit;
        m_brittleAmount = brittleAmount;

        m_name = "Gain Brittle";
        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override string GetDesc()
    {
        return "Gain <b>Brittle</b> " + m_brittleAmount + ".";
    }

    public override void DoAction()
    {
        m_unit.AddKeyword(new GameBrittleKeyword(m_brittleAmount), false);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainBrittleAction tempAction = (GameGainBrittleAction)toAdd;

        m_brittleAmount += tempAction.m_brittleAmount;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainBrittleAction tempAction = (GameGainBrittleAction)toSubtract;

        m_brittleAmount -= tempAction.m_brittleAmount;
    }

    public override bool ShouldBeRemoved()
    {
        return m_brittleAmount <= 0;
    }

    public override JsonActionData SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_brittleAmount
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
