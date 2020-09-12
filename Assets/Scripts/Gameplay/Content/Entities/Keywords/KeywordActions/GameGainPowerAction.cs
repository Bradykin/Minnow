using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainPowerAction : GameAction
{
    private GameEntity m_entity;
    private int m_toGain;

    public GameGainPowerAction(GameEntity entity, int toGain)
    {
        m_entity = entity;
        m_toGain = toGain;

        m_name = "Gain Power";
        m_desc = "+ " + m_toGain + " power";
        m_actionParamType = ActionParamType.EntityIntParam;
    }

    public override void DoAction()
    {
        m_entity.AddPower(m_toGain);
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
