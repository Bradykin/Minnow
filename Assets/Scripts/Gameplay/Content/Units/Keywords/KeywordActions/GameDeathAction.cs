using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDeathAction : GameAction
{
    private GameUnit m_unit;

    public GameDeathAction(GameUnit unit)
    {
        m_unit = unit;

        m_name = "Die";
        m_desc = "Die.";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override string GetDesc()
    {
        return "Die.";
    }

    public override void DoAction()
    {
        m_unit.Die();
    }

    public override void AddAction(GameAction toAdd)
    {
        //Stacking this action does nothing.
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        //Stacking this keyword does nothing.
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
