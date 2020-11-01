using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainEnergyAction : GameAction
{
    private int m_toGain;

    public GameGainEnergyAction(int toGain)
    {
        m_toGain = toGain;

        m_name = "Gain Energy";
        m_actionParamType = ActionParamType.IntParam;
    }

    public override string GetDesc()
    {
        return "Gain " + m_toGain + " energy.";
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.AddEnergy(m_toGain);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainEnergyAction tempAction = (GameGainEnergyAction)toAdd;

        m_toGain += tempAction.m_toGain;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainEnergyAction tempAction = (GameGainEnergyAction)toSubtract;

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

    public override JsonActionData SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_toGain
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
