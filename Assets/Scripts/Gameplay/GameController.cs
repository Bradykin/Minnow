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

public enum RunStateType : int
{
    None,
    Gameplay,
    Intermission
}

public class GameController : ISave<JsonGameControllerData>, ILoad<JsonGameControllerData>
{
    public List<ITurns> m_teamActor;
    public GamePlayer m_player;
    public GameOpponent m_gameOpponent;

    public ITurns CurrentActor => m_teamActor[m_currentActorIterator];
    private int m_currentActorIterator = 0;

    private bool ShouldStartIntermission => CurrentActor == m_player && m_currentTurnNumber > m_endOfWaveTurnNumber && m_currentWaveNumber != Constants.FinalWaveNum;

    public RunStateType m_runStateType = RunStateType.None;

    public int m_currentWaveNumber;
    public int m_currentTurnNumber;
    private int m_endOfWaveTurnNumber;

    public GameMap m_map;

    private int m_runExperienceAmount;
    public int m_randomSeed;

    //Variables used for unit card offerings in intermissions
    public int m_numRareUnitOptionsOffered = 0;
    public int m_previousRareUnitOptionWave = 0;

    //Save data variables - find a better place for these
    public bool m_savedInIntermission;
    public GameCard m_intermissionSavedCardOne;
    public GameCard m_intermissionSavedCardTwo;
    public GameCard m_intermissionSavedCardThree;

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
        m_player.OnBeginWave();
        m_gameOpponent.OnBeginWave();

        m_randomSeed = (int)System.DateTime.Now.Ticks;
        Random.InitState(m_randomSeed);

        m_currentActorIterator = 0;
        m_runStateType = RunStateType.Gameplay;
        CurrentActor.StartTurn();
    }

    public void LoadGameEnterTurnSequence()
    {
        m_currentActorIterator = 0;

        if (m_savedInIntermission)
        {
            CheckStartIntermission(true);
        }
        else
        {
            m_runStateType = RunStateType.Gameplay;
            Globals.loadingRun = false;
        }
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

    public bool CheckStartIntermission(bool forceIntermission = false)
    {
        if (forceIntermission)
        {
            StartIntermissionImpl(false);
            return true;
        }
        
        if (ShouldStartIntermission)
        {
            StartIntermissionImpl();
            return true;
        }

        return false;
    }

    public void StartIntermissionCheat()
    {
        StartIntermissionImpl();
    }

    private void StartIntermissionImpl(bool shouldEndWave = true)
    {
        m_runStateType = RunStateType.Intermission;
        if (shouldEndWave)
        {
            OnEndWave();
        }
        WorldController.Instance.StartIntermission();
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

    public void EndLevel(RunEndType endType)
    {
        if (endType != RunEndType.Quit)
        {
            PlayerDataManager.UpdatePlayerAccountDataOnEndRun(endType, GetRunExperienceNum(), m_map.m_id, Globals.m_curChaos);
            Files.ExportPlayerAccountData(PlayerDataManager.PlayerAccountData);
        }
    }

    public int GetRunExperienceNum()
    {
        return Mathf.Max(50, m_runExperienceAmount);
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

            numRareUnitOptionsOffered = m_numRareUnitOptionsOffered,
            previousRareUnitOptionWave = m_previousRareUnitOptionWave,

            savedInIntermission = m_savedInIntermission,
            jsonGamePlayerData = m_player.SaveToJson(),
            jsonGameOpponentData = m_gameOpponent.SaveToJson()
        };

        if (m_savedInIntermission)
        {
            jsonData.jsonIntermissionCardDataOne = m_intermissionSavedCardOne.SaveToJson();
            jsonData.jsonIntermissionCardDataTwo = m_intermissionSavedCardTwo.SaveToJson();
            jsonData.jsonIntermissionCardDataThree = m_intermissionSavedCardThree.SaveToJson();
        }

        return jsonData;
    }

    public void LoadFromJson(JsonGameControllerData jsonData)
    {
        m_currentWaveNumber = jsonData.currentWave;
        m_currentTurnNumber = jsonData.currentTurn;
        m_runExperienceAmount = jsonData.runExperienceAMount;

        m_numRareUnitOptionsOffered = jsonData.numRareUnitOptionsOffered;
        m_previousRareUnitOptionWave = jsonData.previousRareUnitOptionWave;

        m_savedInIntermission = jsonData.savedInIntermission;

        if (m_savedInIntermission)
        {
            m_intermissionSavedCardOne = GameCardFactory.GetCardFromJson(jsonData.jsonIntermissionCardDataOne);
            m_intermissionSavedCardTwo = GameCardFactory.GetCardFromJson(jsonData.jsonIntermissionCardDataTwo);
            m_intermissionSavedCardThree = GameCardFactory.GetCardFromJson(jsonData.jsonIntermissionCardDataThree);
        }

        m_randomSeed = jsonData.randomSeed;
        Random.InitState(m_randomSeed);

        m_player.LoadFromJson(jsonData.jsonGamePlayerData);
        m_gameOpponent.LoadFromJson(jsonData.jsonGameOpponentData);
    }
}
