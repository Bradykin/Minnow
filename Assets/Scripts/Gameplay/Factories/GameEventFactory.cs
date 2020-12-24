using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventFactory
{
    private static List<GameEvent> m_events = new List<GameEvent>();

    private static List<GameEvent> m_recentEvents = new List<GameEvent>();
    private static int m_numTrackRecentEvents = 5;

    public static void Init(List<GameEvent> events)
    {
        m_events = events;
    }

    public static void DeInit()
    {
        m_events.Clear();

    }

    public static GameEvent GetRandomEvent(GameTile tile)
    {
        if (GameHelper.IsInLevelBuilder())
        {
            return null;
        }
        
        List<GameEvent> availableEvents = new List<GameEvent>();

        for (int i = 0; i < m_events.Count; i++)
        {
            if (m_recentEvents.Contains(m_events[i]))
            {
                continue;
            }

            if (m_events[i].m_rarity == GameElementBase.GameRarity.Special)
            {
                continue;
            }

            availableEvents.Add(m_events[i]);
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
        int i = m_events.FindIndex(t => t.GetName() == jsonData.name);

        GameEvent newEvent = (GameEvent)Activator.CreateInstance(m_events[i].GetType(), tile);
        newEvent.LoadFromJson(jsonData);

        return newEvent;
    }
}