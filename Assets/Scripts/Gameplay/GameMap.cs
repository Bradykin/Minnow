﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMap : GameElementBase
{
    public int m_id;
    public MapDifficulty m_difficulty;

    private List<GameMapEvent> m_mapEvents = new List<GameMapEvent>();
    private List<int> m_mapEventTriggerWaves = new List<int>();

    protected List<GameEnemyUnit> m_totalEnemiesOnMap = new List<GameEnemyUnit>();
    protected GameSpawnPool m_defaultSpawnPool;
    protected List<GameSpawnPool> m_spawnPointSpawnPools = new List<GameSpawnPool>();

    protected List<GameCard> m_exclusionCardPool = new List<GameCard>();
    protected List<GameEvent> m_eventPool = new List<GameEvent>();
    protected List<GameRelic> m_exclusionRelicPool = new List<GameRelic>();

    protected int m_destroyedCrystals;

    public bool m_disableUnfinished;
    public bool m_haveInitialFogSpawned = true;

    public AudioClip m_backgroundMusic;

    protected void Init()
    {
        m_icon = UIHelper.GetIconMap(m_name);
        m_backgroundMusic = AudioHelper.GetBackgroundMusic(m_name);
    }

    public string GetDesc()
    {
        return m_desc;
    }

    public void TriggerStartMap()
    {
        FillSpawnPool();
        GameUnitFactory.Init(m_totalEnemiesOnMap, m_defaultSpawnPool, m_spawnPointSpawnPools);

        FillExclusionCardPool();
        GameCardFactory.Init(m_exclusionCardPool);

        FillExclusionRelicPool();
        GameRelicFactory.Init(m_exclusionRelicPool);

        FillEventPool();
        GameEventFactory.Init(m_eventPool);

        FillMapEvents();
    }


    public abstract int GetNumEnemiesToSpawn();

    public virtual void DestroyCrystal()
    {
        m_destroyedCrystals++;
    }
    
    /*public virtual bool AllCrystalsDestroyed()
    {
        if (!m_spawnCrystals)
        {
            return true;
        }

        if (m_destroyedCrystals == GetNumCrystals())
        {
            return true;
        }

        return false;
    }*/

    protected abstract void FillMapEvents();

    protected void AddMapEvent(GameMapEvent toAdd, int triggerWave)
    {
        m_mapEvents.Add(toAdd);
        m_mapEventTriggerWaves.Add(triggerWave);
    }

    public void TriggerMapEvents(int waveNum, ScheduledActionTime triggerType)
    {
        for (int i = 0; i < m_mapEvents.Count; i++)
        {
            if (m_mapEventTriggerWaves[i] == waveNum && m_mapEvents[i].m_triggerType == triggerType)
            {
                m_mapEvents[i].TriggerEvent();
                UIHelper.CreateHUDNotification(m_mapEvents[i].GetName(), m_mapEvents[i].GetDesc());
            }
        }
    }

    protected abstract void FillSpawnPool();

    protected abstract void FillExclusionCardPool();
    protected abstract void FillEventPool();
    protected abstract void FillExclusionRelicPool();

    protected void FillBasicEventPool()
    {
        m_eventPool.Add(new ContentOverturnedCartEvent(null));
        m_eventPool.Add(new ContentRogueEvent(null));
        m_eventPool.Add(new ContentMagicianEvent(null));
        m_eventPool.Add(new ContentOrcDenEvent(null));
        m_eventPool.Add(new ContentGoblinBarricadeEvent(null));
        m_eventPool.Add(new ContentStablesEvent(null));
        m_eventPool.Add(new ContentGoldenFruitEvent(null));
        m_eventPool.Add(new ContentMillitiaEvent(null));
        m_eventPool.Add(new ContentDemonicFireEvent(null));
        m_eventPool.Add(new ContentBurrowOrSwimEvent(null));
        m_eventPool.Add(new ContentForestOrCityEvent(null));
        m_eventPool.Add(new ContentCombatTrainingEvent(null));
        m_eventPool.Add(new ContentGemsOfProphecyEvent(null));
        m_eventPool.Add(new ContentLibraryOfDenumianEvent(null));
        m_eventPool.Add(new ContentTraditionOrProgressEvent(null));
        m_eventPool.Add(new ContentDevilishPowerEvent(null));
        m_eventPool.Add(new ContentRestorationBrickEvent(null));
        m_eventPool.Add(new ContentAngelicGiftEvent(null));

        FillAltars();
    }

    protected virtual void FillAltars()
    {
        m_eventPool.Add(new ContentTelloAltar(null));
        m_eventPool.Add(new ContentMonAltar(null));
        m_eventPool.Add(new ContentDorphinAltar(null));
        m_eventPool.Add(new ContentSugoAltar(null));
    }

    /*public virtual int GetNumCrystals()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            return 2;
        }

        return 1;
    }

    public bool GetShouldSpawnCrystals()
    {
        return m_spawnCrystals;
    }*/

    public virtual bool TrySpawnElite(List<GameTile> tilesAtFogEdge)
    {
        GameOpponent gameOpponent = GameHelper.GetOpponent();

        if (gameOpponent.m_hasSpawnedEliteThisWave)
        {
            return true;
        }

        if (GameHelper.GetGameController().m_currentTurnNumber < (gameOpponent.m_eliteSpawnWaveModifier + Constants.SpawnEliteTurn))
        {
            return false;
        }

        GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetRandomEliteEnemy(gameOpponent);
        if (gameOpponent.TryForceSpawnAtEdgeOfFog(gameEnemyUnit, tilesAtFogEdge))
        {
            return true;
        }

        //Failed to spawn at fog edge, instead spawn at a spawn point
        for (int i = 0; i < gameOpponent.m_spawnPoints.Count; i++)
        {
            GameSpawnPoint temp = gameOpponent.m_spawnPoints[i];
            int randomIndex = UnityEngine.Random.Range(i, gameOpponent.m_spawnPoints.Count);
            gameOpponent.m_spawnPoints[i] = gameOpponent.m_spawnPoints[randomIndex];
            gameOpponent.m_spawnPoints[randomIndex] = temp;
        }

        if (GameHelper.GetGameController().m_currentTurnNumber >= (gameOpponent.m_eliteSpawnWaveModifier + Constants.SpawnEliteTurn))
        {
            for (int i = 0; i < gameOpponent.m_spawnPoints.Count; i++)
            {
                if (!gameOpponent.m_spawnPoints[i].m_tile.IsPassable(gameEnemyUnit, false))
                {
                    continue;
                }

                if (gameOpponent.TryForceSpawnAtSpawnPoint(gameEnemyUnit, gameOpponent.m_spawnPoints[i]))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public virtual bool TrySpawnBoss(List<GameTile> tilesAtFogEdge)
    {
        GameOpponent gameOpponent = GameHelper.GetOpponent();

        if (gameOpponent.m_hasSpawnedBoss)
        {
            return true;
        }

        if (GameHelper.GetGameController().m_currentTurnNumber < Constants.SpawnBossTurn || GameHelper.GetCurrentWaveNum() != Constants.FinalWaveNum)
        {
            return false;
        }

        GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetRandomBossEnemy(gameOpponent);
        if (gameOpponent.TryForceSpawnAtEdgeOfFog(gameEnemyUnit, tilesAtFogEdge))
        {
            return true;
        }

        //Failed to spawn at fog edge, instead spawn at a spawn point
        for (int i = 0; i < gameOpponent.m_spawnPoints.Count; i++)
        {
            GameSpawnPoint temp = gameOpponent.m_spawnPoints[i];
            int randomIndex = UnityEngine.Random.Range(i, gameOpponent.m_spawnPoints.Count);
            gameOpponent.m_spawnPoints[i] = gameOpponent.m_spawnPoints[randomIndex];
            gameOpponent.m_spawnPoints[randomIndex] = temp;
        }

        for (int i = 0; i < gameOpponent.m_spawnPoints.Count; i++)
        {
            if (!gameOpponent.m_spawnPoints[i].m_tile.IsPassable(gameEnemyUnit, false))
            {
                continue;
            }

            if (gameOpponent.TryForceSpawnAtSpawnPoint(gameEnemyUnit, gameOpponent.m_spawnPoints[i]))
            {
                return true;
            }
        }

        return false;
    }

    public virtual List<GameTile> GetValidFogSpawningTiles()
    {
        return WorldGridManager.Instance.GetFogBorderGameTiles();
    }
}
