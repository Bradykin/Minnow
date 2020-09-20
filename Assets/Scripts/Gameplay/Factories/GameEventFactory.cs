﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventFactory
{
    private static List<GameEvent> m_events = new List<GameEvent>();

    private static List<GameEvent> m_recentEvents = new List<GameEvent>();
    private static int m_numTrackRecentEvents = 3;

    private static bool m_hasInit = false;

    public static void Init()
    {
        m_events.Add(new ContentWonderousGenieEvent(null));
        m_events.Add(new ContentOverturnedCartEvent(null));
        m_events.Add(new ContentMillitiaEvent(null));
        m_events.Add(new ContentAngelicGiftEvent(null));
        m_events.Add(new ContentClericEvent(null));
        m_events.Add(new ContentRogueEvent(null));
        m_events.Add(new ContentMysteryWanderer(null));
        m_events.Add(new ContentStablesEvent(null));
        m_events.Add(new ContentMagicianEvent(null));
        m_events.Add(new ContentGemsOfProphecyEvent(null));
        //m_events.Add(new ContentOrcDenEvent(null)); nmartino - make this only spawn on the waves at or after the point where orcs start spawning
        m_events.Add(new ContentForbiddenFruitEvent(null));
        //m_events.Add(new ContentCreativeChemistEvent(null)); nmartino - add this in after event rework. Make it only spawn if you have the gold for at least the second option? Maybe unless you have all of them
        m_events.Add(new ContentTraditionOrProgressEvent(null));
        //m_events.Add(new ContentWorthySacrificeEvent(null)); ashulman - I wasn't sure how to get this working, need to consult with nmartino to finish this

        //m_events.Add(new ContentTransmuteBeamsEvent(null)); nmartino - Come back to this one event refactor is complete

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
            return (GameEvent)Activator.CreateInstance(m_events[UnityEngine.Random.Range(0, m_events.Count)].GetType(), tile);
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