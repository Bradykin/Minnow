using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainTempMagicPowerAction : GameAction
{
    private int m_toGain;

    public GameGainTempMagicPowerAction(int toGain)
    {
        m_toGain = toGain;

        m_name = "Gain Temporary Magic Power";
        m_actionParamType = ActionParamType.IntParam;
    }

    public override string GetDesc()
    {
        return "+ " + m_toGain + " <b>Magic Power</b> until end of wave.";
    }

    public override void DoAction()
    {
        GameHelper.GetPlayer().m_tempMagicPowerIncrease += m_toGain;
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainTempMagicPowerAction tempAction = (GameGainTempMagicPowerAction)toAdd;

        m_toGain += tempAction.m_toGain;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainTempMagicPowerAction tempAction = (GameGainTempMagicPowerAction)toSubtract;

        m_toGain -= tempAction.m_toGain;
    }

    public override bool ShouldBeRemoved()
    {
        return m_toGain <= 0;
    }

    public override GameUnit GetGameUnit()
    {
        return null;
    }

    public override JsonGameActionData SaveToJson()
    {
        JsonGameActionData jsonData = new JsonGameActionData
        {
            name = m_name,
            intValue1 = m_toGain
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
