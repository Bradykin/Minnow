using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RunEndType : int
{
    Win,
    Loss,
    Quit
}

public class GameController : ISave<JsonGameControllerData>, ILoad<JsonGameControllerData>
{
    public List<ITurns> m_teamActor;
    public GamePlayer m_player;
    public GameOpponent m_gameOpponent;

    public ITurns CurrentActor => m_teamActor[m_currentActorIterator];
    private int m_currentActorIterator = 0;

    private bool ShouldStartIntermission => CurrentActor == m_player && m_currentTurnNumber > m_endOfWaveTurnNumber && m_currentWaveNumber != Constants.FinalWaveNum;

    public bool m_inGameplay = false;

    public int m_currentWaveNumber;
    public int m_currentTurnNumber;
    private int m_endOfWaveTurnNumber;

    public GameMap m_map;

    private int m_runExperienceAmount;
    public int m_randomSeed;

    public GameController(GameMap map)
    {
        m_player = new GamePlayer();
        m_gameOpponent = new GameOpponent();
        m_teamActor = new List<ITurns>();
        m_teamActor.Add(m_player);
        m_teamActor.Add(m_gameOpponent);

        m_currentWaveNumber = 1;
        m_currentTurnNumber = 1;
        m_endOfWaveTurnNumber = Constants.GetWaveLength(m_currentWaveNumber);

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
        if (Globals.loadingRun)
        {
            LoadGameEnterTurnSequence();
        }
        else
        {
            BeginTurnSequence();
        }
    }

    public void BeginTurnSequence()
    {
        if (GameHelper.RelicCount<ContentTotemOfTheWolfRelic>() > 0)
        {
            Globals.m_totemOfTheWolfTurn = Random.Range(0, GetEndWaveTurn() + 1);
        }

        m_randomSeed = (int)System.DateTime.Now.Ticks;
        Random.InitState(m_randomSeed);

        m_currentActorIterator = 0;
        m_inGameplay = true;
        m_gameOpponent.EliteSpawnWaveModifier = Random.Range(0, 3);
        CurrentActor.StartTurn();
    }

    public void LoadGameEnterTurnSequence()
    {
        m_currentActorIterator = 0;
        m_inGameplay = true;
    }

    public void MoveToNextTurn()
    {
        CurrentActor.EndTurn();

        if (m_currentActorIterator == m_teamActor.Count - 1)
        {
            m_currentTurnNumber++;
            m_currentActorIterator = 0;
        }
        else
        {
            m_currentActorIterator++;
        }

        bool didStartIntermission = CheckStartIntermission();
        if (didStartIntermission)
        {
            return;
        }

        CurrentActor.StartTurn();
    }

    public bool CheckStartIntermission()
    {
        if (ShouldStartIntermission)
        {
            m_inGameplay = false;
            OnEndWave();
            WorldController.Instance.StartIntermission();
            return true;
        }

        return false;
    }

    private void OnEndWave()
    {
        GetCurMap().TriggerMapEvents(m_currentWaveNumber, ScheduledActionTime.EndOfWave);
        m_currentWaveNumber++;
        m_currentTurnNumber = 1;
        m_endOfWaveTurnNumber = Constants.GetWaveLength(m_currentWaveNumber);
    }

    public int GetEndWaveTurn()
    {
        int toReturn = m_endOfWaveTurnNumber;

        return toReturn;
    }

    public void AddRunExperience(int experienceAmount)
    {
        m_runExperienceAmount += experienceAmount;
    }

    public void OnEndRun(RunEndType endType)
    {
        if (endType != RunEndType.Quit)
        {
            PlayerDataManager.UpdatePlayerAccountDataOnEndRun(endType, Mathf.Max(50, m_runExperienceAmount), m_map.m_id, Globals.m_curChaos);
            Files.ExportPlayerAccountData(PlayerDataManager.PlayerAccountData);
        }
    }


    //============================================================================================================//

    public JsonGameControllerData SaveToJson()
    {
        JsonGameControllerData jsonData = new JsonGameControllerData
        {
            currentWave = m_currentWaveNumber,
            currentTurn = m_currentTurnNumber,
            mapId = GetCurMap().m_id,
            runExperienceAMount = m_runExperienceAmount,
            randomSeed = m_randomSeed,
            jsonGamePlayerData = m_player.SaveToJson()
        };

        return jsonData;
    }

    public void LoadFromJson(JsonGameControllerData jsonData)
    {
        m_currentWaveNumber = jsonData.currentWave;
        m_currentTurnNumber = jsonData.currentTurn;
        m_runExperienceAmount = jsonData.runExperienceAMount;

        m_randomSeed = jsonData.randomSeed;
        Random.InitState(m_randomSeed);

        m_player.LoadFromJson(jsonData.jsonGamePlayerData);
    }
}
