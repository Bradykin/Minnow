using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDrawCardAction : GameAction
{
    private int m_toDraw;

    public GameDrawCardAction(int toDraw)
    {
        m_toDraw = toDraw;

        m_name = "Draw Card";
        m_actionParamType = ActionParamType.IntParam;
    }

    public override string GetDesc()
    {
        return "Draw " + m_toDraw + ".";
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.DrawCards(m_toDraw);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameDrawCardAction tempAction = (GameDrawCardAction)toAdd;

        m_toDraw += tempAction.m_toDraw;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameDrawCardAction tempAction = (GameDrawCardAction)toSubtract;

        m_toDraw -= tempAction.m_toDraw;
    }

    public override bool ShouldBeRemoved()
    {
        return m_toDraw <= 0;
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
            intValue1 = m_toDraw
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
