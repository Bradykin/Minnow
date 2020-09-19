using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventFactory
{
    private static List<GameEvent> m_events = new List<GameEvent>();

    private static bool m_hasInit = false;

    public static void Init()
    {
        /*m_events.Add(new ContentWonderousGenieEvent(null));
        m_events.Add(new ContentOverturnedCartEvent(null));
        m_events.Add(new ContentMillitiaEvent(null));
        m_events.Add(new ContentAngelicGiftEvent(null));
        m_events.Add(new ContentClericEvent(null));
        m_events.Add(new ContentRogueEvent(null));
        m_events.Add(new ContentMysteryWanderer(null));
        m_events.Add(new ContentStablesEvent(null));
        m_events.Add(new ContentMagicianEvent(null));
        m_events.Add(new ContentGemsOfProphecyEvent(null));
        m_events.Add(new ContentOrcDenEvent(null));
        m_events.Add(new ContentForbiddenFruitEvent(null));*/
        m_events.Add(new ContentCreativeChemistEvent(null));

        //m_events.Add(new ContentTransmuteBeamsEvent(null)); nmartino - Come back to this one event refactor is complete

        m_hasInit = true;
    }

    public static GameEvent GetRandomEvent(GameTile tile)
    {
        if (!m_hasInit)
        {
            Init();
        }

        int r = UnityEngine.Random.Range(0, m_events.Count);

        return (GameEvent)Activator.CreateInstance(m_events[r].GetType(), tile);
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