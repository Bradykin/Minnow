using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    public List<ITurns> m_teamTurns;
    public GamePlayer m_player;
    public GameOpponent m_gameOpponent;
    public ITurns m_currentTurn => m_teamTurns[m_currentTurnIndex];

    private bool ShouldStartIntermission => m_currentTurn == m_player && m_player.m_currentWaveTurn > m_player.GetEndWaveTurn() && m_player.m_waveNum != Constants.FinalWaveNum;

    private int m_currentTurnIndex = 0;

    public GameController()
    {
        m_player = new GamePlayer();
        m_gameOpponent = new GameOpponent();
        m_teamTurns = new List<ITurns>();
        m_teamTurns.Add(m_player);
        m_teamTurns.Add(m_gameOpponent);
    }

    public void LateInit()
    {
        WorldGridManager.Instance.SetupWorldGridTeams(m_player, m_gameOpponent);
        m_player.LateInit();
        m_gameOpponent.LateInit();
        //WorldGridManager.Instance.SetupEnemies(m_gameOpponent);

        //TEMP: to start the turns. Should happen in a different way in future
        BeginTurnSequence();
    }

    public void BeginTurnSequence()
    {
        m_currentTurnIndex = 0;
        m_currentTurn.StartTurn();
    }

    public void MoveToNextTurn()
    {
        m_currentTurn.EndTurn();

        if (m_currentTurnIndex == m_teamTurns.Count - 1)
            m_currentTurnIndex = 0;
        else
            m_currentTurnIndex++;

        if (ShouldStartIntermission)
        {
            WorldController.Instance.StartIntermission();
            return;
        }

        m_currentTurn.StartTurn();
    }
}
