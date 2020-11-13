using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFullHealAction : GameAction
{
    private GameUnit m_unit;

    public GameFullHealAction(GameUnit unit)
    {
        m_unit = unit;

        m_name = "Full Heal";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override string GetDesc()
    {
        return "Fully heal.";
    }

    public override void DoAction()
    {
        m_unit.Heal(m_unit.GetMaxHealth());
    }

    public override void AddAction(GameAction toAdd)
    {
        //This doesn't do anything when stacked.  Left empty on purpose.
    }

    public override void SubtractAction(GameAction toAdd)
    {
        //This doesn't do anything when stacked.  Left empty on purpose.
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
