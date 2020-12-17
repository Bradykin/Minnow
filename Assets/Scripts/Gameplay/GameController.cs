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
    public GameDirector GameDirector
    {
        get
        {
            if (m_gameDirector == null)
            {
                m_gameDirector = Files.ImportGameDirectorData();
            }
            return m_gameDirector;
        }
    }
    private GameDirector m_gameDirector;
    
    public List<ITurns> m_teamActor;
    public GamePlayer m_player;
    public GameOpponent m_gameOpponent;

    public ITurns CurrentActor => m_teamActor[m_currentActorIterator];
    private int m_currentActorIterator = 0;

    private bool ShouldStartIntermission => CurrentActor == m_player && m_curKillCount >= m_endWaveKillCount && m_currentWaveNumber != Constants.FinalWaveNum;

    public bool IntermissionLock
    {
        get
        {
            return m_intermissionLockCount > 0;
        }
    }
    private int m_intermissionLockCount;

    public RunStateType m_runStateType = RunStateType.None;

    public int m_currentWaveNumber;
    public int m_currentTurnNumber;

    public GameMap m_map;

    private int m_killExpAmount;
    private int m_killEliteExpAmount;
    private int m_eventExpAmount;
    private int m_baseExpAmount = 50;
    private int m_victoryAmount = 200;
    private int m_firstChaosClearAmount = 500;

    public int m_curKillCount;
    public int m_endWaveKillCount;

    public int m_randomSeed;

    public List<GameEnemyUnit> m_activeBossUnits;

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

        m_activeBossUnits = new List<GameEnemyUnit>();
        m_currentWaveNumber = 1;
        m_endWaveKillCount = Constants.GetWaveKillCount(m_currentWaveNumber);
        m_currentTurnNumber = 1;

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

        GetCurMap().TriggerMapEvents(m_currentWaveNumber, ScheduledActionTime.StartOfWave);

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

        m_currentTurnNumber++;

        if (m_currentActorIterator == m_teamActor.Count - 1)
        {
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
        if (IntermissionLock)
        {
            return false;
        }
        
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

    public void AddIntermissionLock()
    {
        m_intermissionLockCount++;
    }

    public void RemoveIntermissionLock()
    {
        if (IntermissionLock)
        {
            m_intermissionLockCount--;

            if (!IntermissionLock)
            {
                CheckStartIntermission();
            }
        }
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
        m_curKillCount = 0;
        m_endWaveKillCount = Constants.GetWaveKillCount(m_currentWaveNumber);
    }

    public void KillEnemy(bool incrementsKillCounter)
    { 
        if (incrementsKillCounter)
        {
            m_curKillCount++;

            if (m_curKillCount >= m_endWaveKillCount)
            {
                CheckStartIntermission();
            }
            else if (m_endWaveKillCount - m_curKillCount == 3)
            {
                UIHelper.CreateHUDNotification("Nearly There", "Killing a few more ought to force them back and give some more time to build!");
            }
        }
    }

    public void AddKillExp(int experienceAmount)
    {
        m_killExpAmount += experienceAmount;
    }

    public void AddEventExp(int experienceAmount)
    {
        m_eventExpAmount += experienceAmount;
    }

    public void AddEliteExp(int experienceAmount)
    {
        m_killEliteExpAmount += experienceAmount;
    }

    public void EndLevel(RunEndType endType)
    {
        GameUnitFactory.DeInit();
        GameEventFactory.DeInit();
        
        if (endType != RunEndType.Quit)
        {
            bool isVictory = endType == RunEndType.Win;
            bool firstChaosClear = !PlayerDataManager.IsChaosLevelAchieved(GetCurMap().m_id, Globals.m_curChaos);
            PlayerDataManager.UpdatePlayerAccountDataOnEndRun(endType, GetRunExperienceNum(isVictory, firstChaosClear), m_map.m_id, Globals.m_curChaos);
            SaveDirectorData();
            Files.ExportPlayerAccountData(PlayerDataManager.PlayerAccountData);
        }
    }

    public int GetKillExpNum()
    {
        return m_killExpAmount;
    }

    public int GetEventExpNum()
    {
        return m_eventExpAmount;
    }

    public int GetBaseExpNum()
    {
        return m_baseExpAmount;
    }

    public int GetVictoryNum()
    {
        return m_victoryAmount;
    }

    public int GetFirstChaosNum()
    {
        return m_firstChaosClearAmount;
    }

    public int GetEliteExpNum()
    {
        return m_killEliteExpAmount;
    }

    public int GetRunExperienceNum(bool victory, bool firstChaos)
    {
        int toReturn = m_baseExpAmount + m_killExpAmount + m_eventExpAmount + m_killEliteExpAmount;
        
        if (victory)
        {
            toReturn += m_victoryAmount;

            if (firstChaos)
            {
                toReturn += m_firstChaosClearAmount;
            }
        }

        return toReturn;
    }

    public void SaveDirectorData()
    {
        Files.ExportGameDirectorData(GameDirector);
    }

    //============================================================================================================//

    public JsonGameControllerData SaveToJson()
    {
        JsonGameControllerData jsonData = new JsonGameControllerData
        {
            currentWave = m_currentWaveNumber,
            currentTurn = m_currentTurnNumber,
            mapId = GetCurMap().m_id,
            chaosLevel = Globals.m_curChaos,

            endWaveKillCount = m_endWaveKillCount,
            curKillCount = m_curKillCount,

            runBaseExp = m_baseExpAmount,
            runKillExp = m_killExpAmount,
            runEventExp = m_eventExpAmount,
            runEliteExp = m_killEliteExpAmount,
            runVictoryExp = m_victoryAmount,
            runFirstChaosExp = m_firstChaosClearAmount,

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
        Globals.m_curChaos = jsonData.chaosLevel;
        m_endWaveKillCount = jsonData.endWaveKillCount;
        m_curKillCount = jsonData.curKillCount;

        m_baseExpAmount = jsonData.runBaseExp;
        m_killExpAmount = jsonData.runKillExp;
        m_eventExpAmount = jsonData.runEventExp;
        m_killEliteExpAmount = jsonData.runEliteExp;
        m_victoryAmount = jsonData.runVictoryExp;
        m_firstChaosClearAmount = jsonData.runFirstChaosExp;

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
