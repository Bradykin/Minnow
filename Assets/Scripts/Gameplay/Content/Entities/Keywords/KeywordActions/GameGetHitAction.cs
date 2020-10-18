using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGetHitAction : GameAction
{
    private GameUnit m_unit;
    private int m_damage;

    public GameGetHitAction(GameUnit unit, int damage)
    {
        m_unit = unit;
        m_damage = damage;

        m_name = "Get hit";
        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override void DoAction()
    {
        GameBrittleKeyword keyword = m_unit.GetKeyword<GameBrittleKeyword>();

        m_unit.GetHit(m_damage);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGetHitAction tempAction = (GameGetHitAction)toAdd;

        m_damage += tempAction.m_damage;
    }

    public override string GetDesc()
    {
        return "Get hit for " + m_damage + ".";
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_damage
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
