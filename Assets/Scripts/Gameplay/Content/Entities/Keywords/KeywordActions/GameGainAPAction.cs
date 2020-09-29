using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainAPAction : GameAction
{
    private GameEntity m_entity;
    private int m_toGain;

    public GameGainAPAction(GameEntity entity, int toGain)
    {
        m_entity = entity;
        m_toGain = toGain;

        m_name = "Gain AP";
        m_desc = "Gain " + m_toGain + " AP";
        m_actionParamType = ActionParamType.EntityIntParam;
    }

    public override void DoAction()
    {
        m_entity.GainAP(m_toGain);
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
