using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainDamageShieldAction : GameAction
{
    private GameUnit m_unit;
    private int m_damageShieldVal;

    public GameGainDamageShieldAction(GameUnit unit, int damageShieldVal)
    {
        m_unit = unit;
        m_damageShieldVal = damageShieldVal;

        m_name = "Damage Shield";
        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override string GetDesc()
    {
        return "Gain <b>Damage Shield</b> " + m_damageShieldVal + ".";
    }

    public override void DoAction()
    {
        m_unit.AddKeyword(new GameDamageShieldKeyword(m_damageShieldVal), false);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainDamageShieldAction tempAction = (GameGainDamageShieldAction)toAdd;

        m_damageShieldVal += tempAction.m_damageShieldVal;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainDamageShieldAction tempAction = (GameGainDamageShieldAction)toSubtract;

        m_damageShieldVal -= tempAction.m_damageShieldVal;
    }

    public override bool ShouldBeRemoved()
    {
        return m_damageShieldVal <= 0;
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
            intValue1 = m_damageShieldVal
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
