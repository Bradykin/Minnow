using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainGoldEnrageAction : GameAction
{
    private GameUnit m_unit;
    private int m_numTimesToGain = 1;

    public GameGainGoldEnrageAction(GameUnit unit)
    {
        m_unit = unit;

        m_name = "Gain Gold Enrage";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override void DoAction()
    {
        //Left as stub (instead uses DoAction(int))
    }

    public void DoAction(int damageAmount)
    {
        for (int i = 0; i < m_numTimesToGain; i++)
        {
            GameHelper.GetPlayer().m_wallet.m_gold += damageAmount;
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainGoldEnrageAction tempAction = (GameGainGoldEnrageAction)toAdd;

        m_numTimesToGain += tempAction.m_numTimesToGain;
    }

    public override string GetDesc()
    {
        if (m_numTimesToGain == 1)
        {
            return "Gain gold equal to the damage taken.";
        }
        else
        {
            return "Gain gold equal to " + m_numTimesToGain + " times the damage taken.";
        }
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
