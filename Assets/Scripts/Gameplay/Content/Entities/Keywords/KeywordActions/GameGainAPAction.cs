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
        m_desc = "Gain " + m_toGain + " Stamina";
        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override void DoAction()
    {
        m_unit.GainStamina(m_toGain);
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_toGain
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
