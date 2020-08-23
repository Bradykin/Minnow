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
}
