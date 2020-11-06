using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoseTempSpellpowerAction : GameAction
{
    private int m_toLose;

    public GameLoseTempSpellpowerAction(int toLose)
    {
        m_toLose = toLose;

        m_name = "Lose Temporary Spellpower";
        m_actionParamType = ActionParamType.IntParam;
    }

    public override string GetDesc()
    {
        return "- " + m_toLose + " spellpower until end of wave.";
    }

    public override void DoAction()
    {
        GameHelper.GetPlayer().m_tempSpellpowerIncrease -= m_toLose;
    }

    public override void AddAction(GameAction toAdd)
    {
        GameLoseTempSpellpowerAction tempAction = (GameLoseTempSpellpowerAction)toAdd;

        m_toLose += tempAction.m_toLose;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameLoseTempSpellpowerAction tempAction = (GameLoseTempSpellpowerAction)toSubtract;

        m_toLose -= tempAction.m_toLose;
    }

    public override bool ShouldBeRemoved()
    {
        return m_toLose <= 0;
    }

    public override GameUnit GetGameUnit()
    {
        return null;
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
