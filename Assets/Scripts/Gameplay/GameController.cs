using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RunEndType : int
{
    Win,
    Loss,
    Quit
}

public class GameController
{
    public List<ITurns> m_teamTurns;
    public GamePlayer m_player;
    public GameOpponent m_gameOpponent;

    public ITurns m_currentTurn => m_teamTurns[m_currentTurnIndex];

    private bool ShouldStartIntermission => m_currentTurn == m_player && m_currentWaveTurn > m_currentWaveEndTurn && m_waveNum != Constants.FinalWaveNum;

    private int m_currentTurnIndex = 0;
    public bool m_inTurns = false;

    public int m_waveNum;
    public int m_currentWaveTurn;
    private int m_currentWaveEndTurn;

    public GameMap m_map;

    private int m_playthroughExperienceAmount;

    public GameController(GameMap map)
    {
        m_player = new GamePlayer();
        m_gameOpponent = new GameOpponent();
        m_teamTurns = new List<ITurns>();
        m_teamTurns.Add(m_player);
        m_teamTurns.Add(m_gameOpponent);

        m_waveNum = 1;
        m_currentWaveTurn = 1;
        m_currentWaveEndTurn = Constants.GetWaveLength(m_waveNum);

        m_map = map;
    }

    public GameMap GetCurMap()
    {
        return m_map;
    }

    public void LateInit()
    {
        WorldGridManager.Instance.SetupWorldGridTeams(m_player, m_gameOpponent);
        m_player.LateInit();
        m_gameOpponent.LateInit();
        Globals.m_levelActive = true;

        //TEMP: to start the turns. Should happen in a different way in future
        BeginTurnSequence();
    }

    public void BeginTurnSequence()
    {
        if (GameHelper.RelicCount<ContentTotemOfTheWolfRelic>() > 0)
        {
            Globals.m_totemOfTheWolfTurn = Random.Range(0, GetEndWaveTurn() + 1);
        }
        
        m_currentTurnIndex = 0;
        m_inTurns = true;
        m_gameOpponent.EliteSpawnWaveModifier = Random.Range(0, 3);
        m_currentTurn.StartTurn();
    }

    public void MoveToNextTurn()
    {
        m_currentTurn.EndTurn();

        if (m_currentTurnIndex == m_teamTurns.Count - 1)
        {
            m_currentWaveTurn++;
            m_currentTurnIndex = 0;
        }
        else
        {
            m_currentTurnIndex++;
        }

        bool didStartIntermission = CheckStartIntermission();
        if (didStartIntermission)
        {
            return;
        }

        m_currentTurn.StartTurn();
    }

    public bool CheckStartIntermission()
    {
        if (ShouldStartIntermission)
        {
            m_inTurns = false;
            OnEndWave();
            WorldController.Instance.StartIntermission();
            return true;
        }

        return false;
    }

    private void OnEndWave()
    {
        GetCurMap().TriggerMapEvents(m_waveNum, ScheduledActionTime.EndOfWave);
        m_waveNum++;
        m_currentWaveTurn = 1;
        m_currentWaveEndTurn = Constants.GetWaveLength(m_waveNum);
    }

    public int GetEndWaveTurn()
    {
        int toReturn = m_currentWaveEndTurn;

        return toReturn;
    }

    public void AddPlaythroughExperience(int experienceAmount)
    {
        m_playthroughExperienceAmount = experienceAmount;
    }

    public void OnEndPlaythrough(RunEndType endType)
    {
        if (endType != RunEndType.Quit)
        {
            PlayerDataManager.UpdatePlayerAccountDataOnEndRun(endType, Mathf.Max(50, m_playthroughExperienceAmount), m_map.m_id, Globals.m_curChaos);
            Files.ExportPlayerAccountData(PlayerDataManager.PlayerAccountData);
        }
    }
}
