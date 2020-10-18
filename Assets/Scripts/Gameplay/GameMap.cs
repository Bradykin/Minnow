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

    protected void Init()
    {
        m_icon = UIHelper.GetIconMap(m_name);
    }

    public string GetDesc()
    {
        return m_desc;
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
        return 10 + GameHelper.GetGameController().m_waveNum;
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
        m_eventPool.Add(new ContentWonderousGenieEvent(null)); // waves 1-6
        m_eventPool.Add(new ContentOverturnedCartEvent(null)); // waves 1-3 - potential early script
        m_eventPool.Add(new ContentMillitiaEvent(null)); // waves 2-4
        m_eventPool.Add(new ContentAngelicGiftEvent(null)); // waves 2-6
        m_eventPool.Add(new ContentClericEvent(null)); // waves 1-6 - potential early script
        m_eventPool.Add(new ContentRogueEvent(null)); // waves 1-6 - potential early script
        m_eventPool.Add(new ContentMysteryWanderer(null)); // waves 2-6
        m_eventPool.Add(new ContentStablesEvent(null)); // waves 2-5
        m_eventPool.Add(new ContentMagicianEvent(null)); // waves 1-6 - potential early script
        m_eventPool.Add(new ContentGemsOfProphecyEvent(null)); // waves 3-5
        m_eventPool.Add(new ContentOrcDenEvent(null)); // waves that orcs can spawn in - waves 3-4
        m_eventPool.Add(new ContentForbiddenFruitEvent(null)); // waves 3-4
        m_eventPool.Add(new ContentCreativeChemistEvent(null)); // waves 1-6 only if you have gold to spend
        m_eventPool.Add(new ContentTraditionOrProgressEvent(null)); // waves 1-4
    }

    public int GetPlayerUnlockLevel()
    {
        return m_playerUnlockLevel;
    }
}
