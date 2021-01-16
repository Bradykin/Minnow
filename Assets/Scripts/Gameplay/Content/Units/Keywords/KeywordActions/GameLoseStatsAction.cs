using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoseStatsAction : GameAction
{
    private GameUnit m_unit;
    private int m_attackToLose;
    private int m_healthToLose;

    public GameLoseStatsAction(GameUnit unit, int attackToLose, int healthToLose)
    {
        m_unit = unit;
        m_attackToLose = attackToLose;
        m_healthToLose = healthToLose;

        m_name = "Lose Stats";

        m_actionParamType = ActionParamType.UnitTwoIntParam;
    }

    public override string GetDesc()
    {
        return "-" + m_attackToLose + "/-" + m_healthToLose + ".";
    }

    public override void DoAction()
    {
        m_unit.RemoveStats(m_attackToLose, m_healthToLose, false);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameLoseStatsAction tempAction = (GameLoseStatsAction)toAdd;

        m_attackToLose += tempAction.m_attackToLose;
        m_healthToLose += tempAction.m_healthToLose;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameLoseStatsAction tempAction = (GameLoseStatsAction)toSubtract;

        m_attackToLose -= tempAction.m_attackToLose;
        m_healthToLose -= tempAction.m_healthToLose;
    }

    public override bool ShouldBeRemoved()
    {
        return m_attackToLose <= 0 && m_healthToLose <= 0;
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
            intValue1 = m_attackToLose,
            intValue2 = m_healthToLose
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
