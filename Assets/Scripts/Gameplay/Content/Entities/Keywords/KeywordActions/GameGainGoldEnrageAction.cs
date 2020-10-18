using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainGoldEnrageAction : GameAction
{
    private GameUnit m_unit;
    private int m_numTimesToGain;

    public GameGainGoldEnrageAction(GameUnit unit)
    {
        m_unit = unit;

        m_name = "Gain Gold Enrage";
        m_desc = "Gain gold equal to the damage taken.";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override void DoAction()
    {

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
