using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDoubleAttackAction : GameAction
{
    private GameUnit m_unit;
    private int m_numTimesToTrigger;

    public GameDoubleAttackAction(GameUnit unit, int numTimesToTrigger)
    {
        m_unit = unit;
        m_numTimesToTrigger = numTimesToTrigger;

        m_name = "Double Attack";

        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override string GetDesc()
    {
        if (m_numTimesToTrigger == 1)
        {
            return "Double this unit's attack.";
        }
        else
        {
            return "Double this unit's attack " + m_numTimesToTrigger + " times.";
        }
    }

    public override void DoAction()
    {
        for (int i = 0; i < m_numTimesToTrigger; i++)
        {
            m_unit.AddStats(m_unit.GetAttack(), 0, false, true);
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameDoubleAttackAction tempAction = (GameDoubleAttackAction)toAdd;

        m_numTimesToTrigger += tempAction.m_numTimesToTrigger;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameDoubleAttackAction tempAction = (GameDoubleAttackAction)toSubtract;

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
