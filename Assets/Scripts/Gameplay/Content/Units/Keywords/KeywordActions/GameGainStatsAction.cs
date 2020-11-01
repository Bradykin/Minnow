﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainStatsAction : GameAction
{
    private GameUnit m_unit;
    private int m_powerToGain;
    private int m_healthToGain;

    public GameGainStatsAction(GameUnit unit, int powerToGain, int healthToGain)
    {
        m_unit = unit;
        m_powerToGain = powerToGain;
        m_healthToGain = healthToGain;

        m_name = "Gain Stats";

        m_actionParamType = ActionParamType.UnitTwoIntParam;
    }

    public override void DoAction()
    {
        m_unit.AddStats(m_powerToGain, m_healthToGain);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainStatsAction tempAction = (GameGainStatsAction)toAdd;

        m_powerToGain += tempAction.m_powerToGain;
        m_healthToGain += tempAction.m_healthToGain;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainStatsAction tempAction = (GameGainStatsAction)toSubtract;

        m_powerToGain -= tempAction.m_powerToGain;
        m_healthToGain -= tempAction.m_healthToGain;
    }

    public override bool ShouldBeRemoved()
    {
        return m_powerToGain <= 0 && m_healthToGain <= 0;
    }

    public override string GetDesc()
    {
        return "+" + m_powerToGain + "/+" + m_healthToGain;
    }

    public override JsonActionData SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_powerToGain,
            intValue2 = m_healthToGain
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
