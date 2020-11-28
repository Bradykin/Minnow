using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDoublePowerAction : GameAction
{
    private GameUnit m_unit;
    private int m_numTimesToTrigger;

    public GameDoublePowerAction(GameUnit unit, int numTimesToTrigger)
    {
        m_unit = unit;
        m_numTimesToTrigger = numTimesToTrigger;

        m_name = "Double Power";

        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override string GetDesc()
    {
        if (m_numTimesToTrigger == 1)
        {
            return "Double this unit's power.";
        }
        else
        {
            return "Double this unit's power " + m_numTimesToTrigger + " times.";
        }
    }

    public override void DoAction()
    {
        for (int i = 0; i < m_numTimesToTrigger; i++)
        {
            m_unit.AddStats(m_unit.GetPower(), 0, false, true);
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameDoublePowerAction tempAction = (GameDoublePowerAction)toAdd;

        m_numTimesToTrigger += tempAction.m_numTimesToTrigger;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameDoublePowerAction tempAction = (GameDoublePowerAction)toSubtract;

        m_numTimesToTrigger -= tempAction.m_numTimesToTrigger;
    }

    public override bool ShouldBeRemoved()
    {
        return m_numTimesToTrigger <= 0;
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
            intValue1 = m_numTimesToTrigger
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
