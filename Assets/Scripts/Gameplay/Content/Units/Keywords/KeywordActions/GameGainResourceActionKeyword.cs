﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainResourceAction : GameAction
{
    private GameWallet m_toGain;

    public GameGainResourceAction(GameWallet toGain)
    {
        m_toGain = toGain;

        m_name = "Gain Resources";
        m_actionParamType = ActionParamType.GameWalletParam;
    }

    public override void DoAction()
    {
        GameHelper.GetPlayer().m_wallet.AddResources(m_toGain);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainResourceAction tempAction = (GameGainResourceAction)toAdd;

        m_toGain.AddResources(tempAction.m_toGain);
    }

    public override string GetDesc()
    {
        return "Gain " + m_toGain.ToString() + ".";
    }

    public override JsonActionData SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            gameWalletJsonValue = JsonConvert.SerializeObject(m_toGain)
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
