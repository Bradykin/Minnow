using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoseStatsAction : GameAction
{
    private GameUnit m_unit;
    private int m_powerToLose;
    private int m_healthToLose;

    public GameLoseStatsAction(GameUnit unit, int powerToLose, int healthToLose)
    {
        m_unit = unit;
        m_powerToLose = powerToLose;
        m_healthToLose = healthToLose;

        m_name = "Lose Stats";

        m_actionParamType = ActionParamType.UnitTwoIntParam;
    }

    public override string GetDesc()
    {
        return "-" + m_powerToLose + "/-" + m_healthToLose + ".";
    }

    public override void DoAction()
    {
        m_unit.RemoveStats(m_powerToLose, m_healthToLose);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameLoseStatsAction tempAction = (GameLoseStatsAction)toAdd;

        m_powerToLose += tempAction.m_powerToLose;
        m_healthToLose += tempAction.m_healthToLose;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameLoseStatsAction tempAction = (GameLoseStatsAction)toSubtract;

        m_powerToLose -= tempAction.m_powerToLose;
        m_healthToLose -= tempAction.m_healthToLose;
    }

    public override bool ShouldBeRemoved()
    {
        return m_powerToLose <= 0 && m_healthToLose <= 0;
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
            intValue1 = m_powerToLose,
            intValue2 = m_healthToLose
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
