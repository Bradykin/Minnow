﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainTempSpellpowerAction : GameAction
{
    private int m_toGain;

    public GameGainTempSpellpowerAction(int toGain)
    {
        m_toGain = toGain;

        m_name = "Gain Temporary Spellpower";
        m_actionParamType = ActionParamType.IntParam;
    }

    public override void DoAction()
    {
        Globals.m_tempSpellpower += m_toGain;
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainTempSpellpowerAction tempAction = (GameGainTempSpellpowerAction)toAdd;

        m_toGain += tempAction.m_toGain;
    }

    public override string GetDesc()
    {
        return "+ " + m_toGain + " spellpower until end of turn";
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_toGain
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
