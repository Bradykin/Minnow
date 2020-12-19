using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainStaminaAction : GameAction
{
    private GameUnit m_unit;
    private int m_toGain;

    public GameGainStaminaAction(GameUnit unit, int toGain)
    {
        m_unit = unit;
        m_toGain = toGain;

        m_name = "Gain Stamina";
        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override string GetDesc()
    {
        return "Gain " + m_toGain + " Stamina";
    }

    public override void DoAction()
    {
        AudioHelper.PlaySFX(AudioHelper.SmallBuff);
        m_unit.GainStamina(m_toGain);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainStaminaAction tempAction = (GameGainStaminaAction)toAdd;

        m_toGain += tempAction.m_toGain;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainStaminaAction tempAction = (GameGainStaminaAction)toSubtract;

        m_toGain -= tempAction.m_toGain;
    }

    public override bool ShouldBeRemoved()
    {
        return m_toGain <= 0;
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
            intValue1 = m_toGain
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
