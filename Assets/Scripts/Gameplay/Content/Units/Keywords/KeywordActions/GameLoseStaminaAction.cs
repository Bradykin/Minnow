using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoseStaminaAction : GameAction
{
    private GameUnit m_unit;
    private int m_toLose;

    public GameLoseStaminaAction(GameUnit unit, int toLose)
    {
        m_unit = unit;
        m_toLose = toLose;

        m_name = "Lose Stamina";
        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override string GetDesc()
    {
        return "Lose " + m_toLose + " Stamina";
    }

    public override void DoAction()
    {
        m_unit.SpendStamina(m_toLose);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameLoseStaminaAction tempAction = (GameLoseStaminaAction)toAdd;

        m_toLose += tempAction.m_toLose;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameLoseStaminaAction tempAction = (GameLoseStaminaAction)toSubtract;

        m_toLose -= tempAction.m_toLose;
    }

    public override bool ShouldBeRemoved()
    {
        return m_toLose <= 0;
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
            intValue1 = m_toLose
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
