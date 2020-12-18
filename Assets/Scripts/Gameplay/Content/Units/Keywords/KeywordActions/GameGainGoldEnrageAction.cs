using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainGoldEnrageAction : GameAction
{
    private GameUnit m_unit;
    private int m_numTimesToGain = 1;

    public GameGainGoldEnrageAction(GameUnit unit, int numTimesToGain)
    {
        m_unit = unit;
        m_numTimesToGain = numTimesToGain;

        m_name = "Gain Gold Enrage";
        m_actionParamType = ActionParamType.UnitIntParam;
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

    public override void DoAction()
    {
        //Left as stub (instead uses DoAction(int))
    }

    public void DoAction(int damageAmount)
    {
        for (int i = 0; i < m_numTimesToGain; i++)
        {
            GameHelper.GetPlayer().GainGold(damageAmount);
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainGoldEnrageAction tempAction = (GameGainGoldEnrageAction)toAdd;

        m_numTimesToGain += tempAction.m_numTimesToGain;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainGoldEnrageAction tempAction = (GameGainGoldEnrageAction)toSubtract;

        m_numTimesToGain -= tempAction.m_numTimesToGain;
    }

    public override bool ShouldBeRemoved()
    {
        return m_numTimesToGain <= 0;
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
            intValue1 = m_numTimesToGain
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
