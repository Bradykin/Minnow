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
    protected List<GameCard> m_exclusionCardPool = new List<GameCard>();
    protected List<GameEvent> m_eventPool = new List<GameEvent>();
    protected List<GameRelic> m_relicPool = new List<GameRelic>();

    protected void Init()
    {
        m_icon = UIHelper.GetIconMap(m_name);
    }

    public void TriggerStartMap()
    {
        FillSpawnPool();
        GameUnitFactory.Init(m_spawnPool);

        FillCardPool();
        GameCardFactory.Init();

        FillRelicPool();
        GameRelicFactory.Init(m_relicPool);

        FillEventPool();
        GameEventFactory.Init(m_eventPool);

        FillMapEvents();
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

        for (int i = 0; i < m_mapEvents.Count; i++)
        {
            if (m_mapEventTriggerWaves[i] == waveNum- 1 && m_mapEvents[i].m_triggerType == triggerType)
            {
                m_mapEvents[i].EndEvent();
            }
        }
    }

    protected abstract void FillSpawnPool();
    protected abstract void FillCardPool();
    protected abstract void FillEventPool();
    protected abstract void FillRelicPool();

    protected void FillExclusionCardPool()
    {

    }

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

    protected void FillBasicRelicPool()
    {
        m_relicPool.Add(new ContentBestialWrathRelic());
        m_relicPool.Add(new ContentDominerickRefrainRelic());
        m_relicPool.Add(new ContentHourglassOfSpeedRelic());
        m_relicPool.Add(new ContentMaskOfAgesRelic());
        m_relicPool.Add(new ContentMorlemainsSkullRelic());
        m_relicPool.Add(new ContentMysticRuneRelic());
        m_relicPool.Add(new ContentOrbOfEnergyRelic());
        m_relicPool.Add(new ContentOrbOfHealthRelic());
        m_relicPool.Add(new ContentSecretSoupRelic());
        m_relicPool.Add(new ContentSoulTrapRelic());
        m_relicPool.Add(new ContentSpiritCatcherRelic());
        m_relicPool.Add(new ContentWolvenFangRelic());
        m_relicPool.Add(new ContentSackOfManyShapesRelic());
        m_relicPool.Add(new ContentHoovesOfProductionRelic());
        m_relicPool.Add(new ContentDestinyRelic());
        m_relicPool.Add(new ContentUrbanTacticsRelic());
        m_relicPool.Add(new ContentPinnacleOfFearRelic());
        m_relicPool.Add(new ContentNaturalProtectionRelic());
        m_relicPool.Add(new ContentLoadedChestRelic());
        m_relicPool.Add(new ContentLegendaryFragmentRelic());
        m_relicPool.Add(new ContentTomeOfDuluhainRelic());
        m_relicPool.Add(new ContentLivingStoneRelic());
        m_relicPool.Add(new ContentCursedAmuletRelic());
        m_relicPool.Add(new ContentDesignSchematicsRelic());
        m_relicPool.Add(new ContentMedKitRelic());
        m_relicPool.Add(new ContentLegacyOfMonstersRelic());
        m_relicPool.Add(new ContentGrandPactRelic());
        m_relicPool.Add(new ContentTotemOfTheWolfRelic());
        m_relicPool.Add(new ContentBurningShivsRelic());
        m_relicPool.Add(new ContentPoisonedShivsRelic());
        m_relicPool.Add(new ContentTraditionalMethodsRelic());
        m_relicPool.Add(new ContentNewInvestmentsRelic());
    }
}
