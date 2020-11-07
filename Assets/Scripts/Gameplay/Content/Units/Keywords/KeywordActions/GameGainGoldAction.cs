using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainGoldAction : GameAction
{
    private int m_toGain;

    public GameGainGoldAction(int toGain)
    {
        m_toGain = toGain;

        m_name = "Gain Gold";
        m_actionParamType = ActionParamType.IntParam;
    }

    public override string GetDesc()
    {
        return $"Gain {m_toGain} Gold.";
    }

    public override void DoAction()
    {
        GameHelper.GetPlayer().m_wallet.AddGold(m_toGain);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainGoldAction tempAction = (GameGainGoldAction)toAdd;

        m_toGain += tempAction.m_toGain;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainGoldAction tempAction = (GameGainGoldAction)toSubtract;

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
