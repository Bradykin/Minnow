using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainBrittleAction : GameAction
{
    private GameUnit m_unit;
    private int m_toGain;

    public GameGainBrittleAction(GameUnit unit, int toGain)
    {
        m_unit = unit;
        m_toGain = toGain;

        m_name = "Gain Brittle";
        m_desc = "Gain 'Brittle " + m_toGain + "'.";
        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override void DoAction()
    {
        m_unit.AddKeyword(new GameBrittleKeyword(m_toGain), false);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainBrittleAction tempAction = (GameGainBrittleAction)toAdd;

        m_toGain += tempAction.m_toGain;
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
