using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGetHitAction : GameAction
{
    private GameEntity m_entity;
    private int m_damage;

    public GameGetHitAction(GameEntity entity, int damage)
    {
        m_entity = entity;
        m_damage = damage;

        m_name = "Get hit";
        m_desc = "Get hit for " + m_damage + ".";
        m_actionParamType = ActionParamType.EntityIntParam;
    }

    public override void DoAction()
    {
        GameBrittleKeyword keyword = m_entity.GetKeyword<GameBrittleKeyword>();

        m_entity.GetHit(m_damage);
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_damage
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
