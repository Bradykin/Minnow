using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainStatsPermanentAction : GameAction
{
    private GameUnit m_unit;
    private int m_attackToGain;
    private int m_healthToGain;

    public GameGainStatsPermanentAction(GameUnit unit, int attackToGain, int healthToGain)
    {
        m_unit = unit;
        m_attackToGain = attackToGain;
        m_healthToGain = healthToGain;

        m_name = "Gain Stats Permanent";

        m_actionParamType = ActionParamType.UnitTwoIntParam;
    }

    public override string GetDesc()
    {
        return "+" + m_attackToGain + "/+" + m_healthToGain + " (<b>permanent</b>)";
    }

    public override void DoAction()
    {
        m_unit.AddStats(m_attackToGain, m_healthToGain, true, true);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainStatsPermanentAction tempAction = (GameGainStatsPermanentAction)toAdd;

        m_attackToGain += tempAction.m_attackToGain;
        m_healthToGain += tempAction.m_healthToGain;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainStatsPermanentAction tempAction = (GameGainStatsPermanentAction)toSubtract;

        m_attackToGain -= tempAction.m_attackToGain;
        m_healthToGain -= tempAction.m_healthToGain;
    }

    public override bool ShouldBeRemoved()
    {
        return m_attackToGain <= 0 && m_healthToGain <= 0;
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
            intValue1 = m_attackToGain,
            intValue2 = m_healthToGain
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
