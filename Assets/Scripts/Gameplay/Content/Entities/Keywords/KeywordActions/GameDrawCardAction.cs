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
        m_desc = "Draw " + m_toDraw + ".";
        m_actionParamType = ActionParamType.IntParam;
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

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_toDraw
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
