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

    public override string GetDesc()
    {
        return "Get hit for " + m_damage + ".";
    }

    public override void DoAction()
    {
        GameBrittleKeyword keyword = m_unit.GetBrittleKeyword();

        m_unit.GetHitByAbility(m_damage);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGetHitAction tempAction = (GameGetHitAction)toAdd;

        m_damage += tempAction.m_damage;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGetHitAction tempAction = (GameGetHitAction)toSubtract;

        m_damage -= tempAction.m_damage;
    }

    public override bool ShouldBeRemoved()
    {
        return m_damage <= 0;
    }

    public override GameUnit GetGameUnit()
    {
        return m_unit;
    }

    public override JsonActionData SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_damage
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
