using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainMaxHealthAction : GameAction
{
    private GameUnit m_entity;
    private int m_toGain;

    public GameGainMaxHealthAction(GameUnit entity, int toGain)
    {
        m_entity = entity;
        m_toGain = toGain;

        m_name = "Gain Max Health";
        if (toGain >= 0)
        {
            m_desc = "+0/+" + m_toGain + ".";
        }
        else
        {
            m_desc = "-0/-"+ m_toGain + ".";
        }
        m_actionParamType = ActionParamType.EntityIntParam;
    }

    public override void DoAction()
    {
        m_entity.AddMaxHealth(m_toGain);
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
