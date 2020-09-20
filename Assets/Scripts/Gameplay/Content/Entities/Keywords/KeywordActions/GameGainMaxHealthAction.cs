﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainMaxHealthAction : GameAction
{
    private GameEntity m_entity;
    private int m_toGain;

    public GameGainMaxHealthAction(GameEntity entity, int toGain)
    {
        m_entity = entity;
        m_toGain = toGain;

        m_name = "Gain Max Health";
        if (toGain >= 0)
        {
            m_desc = "+" + m_toGain + " max health";
        }
        else
        {
            m_desc = m_toGain + " max health";
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