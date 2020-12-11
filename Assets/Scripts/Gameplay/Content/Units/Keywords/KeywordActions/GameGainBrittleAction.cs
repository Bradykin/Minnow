using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainBrittleAction : GameAction
{
    private GameUnit m_unit;

    public GameGainBrittleAction(GameUnit unit)
    {
        m_unit = unit;

        m_name = "Gain Brittle";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override string GetDesc()
    {
        return "Gain <b>Brittle</b>.";
    }

    public override void DoAction()
    {
        m_unit.AddKeyword(new GameBrittleKeyword(), false, false);
    }

    public override void AddAction(GameAction toAdd)
    {
        //Stacking this action does nothing.
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        //Subtracting this action does nothing.
    }

    public override bool ShouldBeRemoved()
    {
        return false;
    }

    public override GameUnit GetGameUnit()
    {
        return m_unit;
    }

    public override JsonGameActionData SaveToJson()
    {
        JsonGameActionData jsonData = new JsonGameActionData
        {
            name = m_name
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
