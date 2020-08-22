﻿using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    public List<ITurns> m_teamTurns;
    public GamePlayer m_player;
    public ITurns m_currentTurn => m_teamTurns[m_currentTurnIndex];
    

    private int m_currentTurnIndex = 0;
    

    public GameController()
    {
        m_player = new GamePlayer();
        m_teamTurns = new List<ITurns>();
        m_teamTurns.Add(m_player);
        m_teamTurns.Add(new GameOpponent());

        //TEMP: to start the turns. Should happen in a different way in future
        BeginTurnSequence();
    }

    public void BeginTurnSequence()
    {
        m_currentTurn.StartTurn();
    }

    public void MoveToNextTurn()
    {
        m_currentTurn.EndTurn();
        if (m_currentTurnIndex == m_teamTurns.Count - 1)
            m_currentTurnIndex = 0;
        else
            m_currentTurnIndex++;
        m_currentTurn.StartTurn();
    }
}
