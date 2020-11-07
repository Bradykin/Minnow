using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoseTempMagicPowerAction : GameAction
{
    private int m_toLose;

    public GameLoseTempMagicPowerAction(int toLose)
    {
        m_toLose = toLose;

        m_name = "Lose Temporary Magic Power";
        m_actionParamType = ActionParamType.IntParam;
    }

    public override string GetDesc()
    {
        return "- " + m_toLose + " <b>Magic Power</b> until end of wave.";
    }

    public override void DoAction()
    {
        GameHelper.GetPlayer().m_tempMagicPowerIncrease -= m_toLose;
    }

    public override void AddAction(GameAction toAdd)
    {
        GameLoseTempMagicPowerAction tempAction = (GameLoseTempMagicPowerAction)toAdd;

        m_toLose += tempAction.m_toLose;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameLoseTempMagicPowerAction tempAction = (GameLoseTempMagicPowerAction)toSubtract;

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
