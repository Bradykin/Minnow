using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventFactory
{
    private static List<GameEvent> m_events = new List<GameEvent>();

    private static List<GameEvent> m_recentEvents = new List<GameEvent>();
    private static int m_numTrackRecentEvents = 5;

    private static bool m_hasInit = false;

    public static void Init()
    {
        m_events.Add(new ContentWonderousGenieEvent(null)); // waves 1-6
        m_events.Add(new ContentOverturnedCartEvent(null)); // waves 1-3 - potential early script
        m_events.Add(new ContentMillitiaEvent(null)); // waves 2-4
        m_events.Add(new ContentAngelicGiftEvent(null)); // waves 2-6
        m_events.Add(new ContentClericEvent(null)); // waves 1-6 - potential early script
        m_events.Add(new ContentRogueEvent(null)); // waves 1-6 - potential early script
        m_events.Add(new ContentMysteryWanderer(null)); // waves 2-6
        m_events.Add(new ContentStablesEvent(null)); // waves 2-5
        m_events.Add(new ContentMagicianEvent(null)); // waves 1-6 - potential early script
        m_events.Add(new ContentGemsOfProphecyEvent(null)); // waves 3-5
        m_events.Add(new ContentOrcDenEvent(null)); // waves that orcs can spawn in - waves 3-4
        m_events.Add(new ContentForbiddenFruitEvent(null)); // waves 3-4
        m_events.Add(new ContentCreativeChemistEvent(null)); // waves 1-6 only if you have gold to spend
        m_events.Add(new ContentTraditionOrProgressEvent(null)); // waves 1-4
        m_events.Add(new ContentTransmuteBeamsEvent(null)); // waves 1-6 only if you have 5+ purple beams

        //m_events.Add(new ContentWorthySacrificeEvent(null)); // ashulman - I wasn't sure how to get this working, need to consult with nmartino to finish this

        m_hasInit = true;
    }

    public static GameEvent GetRandomEvent(GameTile tile)
    {
        if (!m_hasInit)
        {
            Init();
        }

        List<GameEvent> availableEvents = new List<GameEvent>();

        for (int i = 0; i < m_events.Count; i++)
        {
            if (m_recentEvents.Contains(m_events[i]))
            {
                continue;
            }
            
            if (m_events[i].IsValidToSpawn())
            {
                availableEvents.Add(m_events[i]);
            }
        }

        if (availableEvents.Count == 0)
        {
            Debug.LogError("All events are not available to spawn. Sending random event instead.");
            if (m_recentEvents.Count > 0)
            {
                m_recentEvents.Clear();
                return GetRandomEvent(tile);
            }
            else
            {
                return (GameEvent)Activator.CreateInstance(m_events[UnityEngine.Random.Range(0, m_events.Count)].GetType(), tile);
            }
        }

        int r = UnityEngine.Random.Range(0, availableEvents.Count);

        m_recentEvents.Add(availableEvents[r]);
        if (m_recentEvents.Count > m_numTrackRecentEvents)
        {
            m_recentEvents.RemoveAt(0);
        }

        return (GameEvent)Activator.CreateInstance(availableEvents[r].GetType(), tile);
    }

    public static GameEvent GetEventFromJson(JsonGameEventData jsonData, GameTile tile)
    {
        if (!m_hasInit)
        {
            Init();
        }

        int i = m_events.FindIndex(t => t.m_name == jsonData.name);

        GameEvent newEvent = (GameEvent)Activator.CreateInstance(m_events[i].GetType(), tile);
        newEvent.LoadFromJson(jsonData);

        return newEvent;
    }
}