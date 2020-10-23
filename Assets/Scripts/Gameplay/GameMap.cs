using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMap : GameElementBase
{
    public int m_id;
    public MapDifficulty m_difficulty;

    private List<GameMapEvent> m_mapEvents = new List<GameMapEvent>();
    private List<int> m_mapEventTriggerWaves = new List<int>();

    protected List<GameEnemyUnit> m_spawnPool = new List<GameEnemyUnit>();
    protected List<List<GameEnemyUnit>> m_specificSpawnPools = new List<List<GameEnemyUnit>>();
    protected List<GameCard> m_exclusionCardPool = new List<GameCard>();
    protected List<GameEvent> m_eventPool = new List<GameEvent>();
    protected List<GameRelic> m_exclusionRelicPool = new List<GameRelic>();

    protected int m_playerUnlockLevel;
    protected bool m_fogSpawningActive = true;

    protected int m_destroyedCrystals;

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

    public bool GetFogSpawningActive()
    {
        return m_fogSpawningActive;
    }

    public void TriggerStartMap()
    {
        FillSpawnPool();
        GameUnitFactory.Init(m_spawnPool, m_specificSpawnPools);

        FillExclusionCardPool();
        GameCardFactory.Init();

        FillExclusionRelicPool();
        GameRelicFactory.Init();

        FillEventPool();
        GameEventFactory.Init(m_eventPool);

        FillMapEvents();
    }


    public virtual int GetNumEnemiesToSpawn()
    {
        return 8;
    }

    public virtual void DestroyCrystal()
    {
        m_destroyedCrystals++;
    }

    public virtual bool AllCrystalsDestroyed()
    {
        if (m_destroyedCrystals == GetNumCrystals())
        {
            return true;
        }

        return false;
    }

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
                UIMapEventController.Instance.AddEvent(m_mapEvents[i]);
            }
        }
    }

    protected abstract void FillSpawnPool();
    protected abstract void FillExclusionCardPool();
    protected abstract void FillEventPool();
    protected abstract void FillExclusionRelicPool();

    protected void FillBasicEventPool()
    {
        //Common
        m_eventPool.Add(new ContentOverturnedCartEvent(null)); // waves 1-6
        m_eventPool.Add(new ContentRogueEvent(null)); // waves 1-6
        m_eventPool.Add(new ContentMagicianEvent(null)); // waves 1-6 
        m_eventPool.Add(new ContentMysteryWanderer(null)); // waves 2-4
        m_eventPool.Add(new ContentOrcDenEvent(null)); // waves 3-4
        m_eventPool.Add(new ContentGoblinBarricadeEvent(null)); // waves 3-5
        m_eventPool.Add(new ContentClericEvent(null)); // waves 4-6

        //Uncommon
        m_eventPool.Add(new ContentStablesEvent(null)); // waves 1-6
        m_eventPool.Add(new ContentGoldenFruitEvent(null)); // waves 1-6
        m_eventPool.Add(new ContentMillitiaEvent(null)); // waves 2-4
        m_eventPool.Add(new ContentDemonicFireEvent(null)); // waves 2-6
        m_eventPool.Add(new ContentWonderousGenieEvent(null)); // waves 2-6
        m_eventPool.Add(new ContentCombatTrainingEvent(null)); // waves 3-6
        m_eventPool.Add(new ContentGemsOfProphecyEvent(null)); // waves 3-6
        m_eventPool.Add(new ContentBurrowOrSwimEvent(null)); // waves 2-6

        //Rare
        m_eventPool.Add(new ContentLibraryOfDenumianEvent(null)); // waves 1-2
        m_eventPool.Add(new ContentTraditionOrProgressEvent(null)); // waves 1-2
        m_eventPool.Add(new ContentDevilishPowerEvent(null)); // waves 2-5
        m_eventPool.Add(new ContentTimeTempleEvent(null)); // waves 3-5
        m_eventPool.Add(new ContentRestorationBrickEvent(null)); // waves 3-5
        m_eventPool.Add(new ContentAngelicGiftEvent(null)); // waves 2-6
    }

    public int GetPlayerUnlockLevel()
    {
        return m_playerUnlockLevel;
    }

    public virtual int GetNumCrystals()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            return 2;
        }

        return 1;
    }
}
