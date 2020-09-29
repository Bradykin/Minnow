using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainBrittleAction : GameAction
{
    private GameEntity m_entity;
    private int m_toGain;

    public GameGainBrittleAction(GameEntity entity, int toGain)
    {
        m_entity = entity;
        m_toGain = toGain;

        m_name = "Gain Brittle";
        m_desc = "Gain 'Brittle " + m_toGain + "'.";
        m_actionParamType = ActionParamType.EntityIntParam;
    }

    public override void DoAction()
    {
        GameBrittleKeyword keyword = m_entity.GetKeyword<GameBrittleKeyword>();

        if (keyword != null)
        {
            keyword.IncreaseAmount(m_toGain);
        }
        else
        {
            m_entity.AddKeyword(new GameBrittleKeyword(m_toGain));
        }
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
