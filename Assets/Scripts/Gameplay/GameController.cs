using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    public GamePlayer m_player;

    public GameController()
    {
        m_player = new GamePlayer();
    }

    public void EndTurn()
    {
        m_player.EndTurn();
    }
}
