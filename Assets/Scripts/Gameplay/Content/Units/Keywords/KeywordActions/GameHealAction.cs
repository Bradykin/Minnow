using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHealAction : GameAction
{
    private GameUnit m_unit;
    private int m_healVal;

    public GameHealAction(GameUnit unit, int healVal)
    {
        m_unit = unit;
        m_healVal = healVal;

        m_name = "Heal";
        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override string GetDesc()
    {
        return "Heal for " + m_healVal + ".";
    }

    public override void DoAction()
    {
        m_unit.Heal(m_healVal);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameHealAction tempAction = (GameHealAction)toAdd;

        m_healVal += tempAction.m_healVal;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameHealAction tempAction = (GameHealAction)toSubtract;

        m_healVal -= tempAction.m_healVal;
    }

    public override bool ShouldBeRemoved()
    {
        return m_healVal <= 0;
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
            intValue1 = m_healVal
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
