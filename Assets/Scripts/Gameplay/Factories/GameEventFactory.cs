using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventFactory
{
    private static List<GameEvent> m_events = new List<GameEvent>();

    private static bool m_hasInit = false;

    public static void Init(GameTile tile)
    {
        m_events.Add(new ContentDragonDenEvent(tile));
        m_events.Add(new ContentWonderousGenieEvent(tile));
        m_events.Add(new ContentOverturnedCartEvent(tile));
        m_events.Add(new ContentMillitiaEvent(tile));
        m_events.Add(new ContentAngelicGiftEvent(tile));
        m_events.Add(new ContentMagicianEvent(tile));
        m_events.Add(new ContentClericEvent(tile));
        m_events.Add(new ContentRogueEvent(tile));
        m_events.Add(new ContentMysteryWanderer(tile));
        m_events.Add(new ContentStablesEvent(tile));
        m_events.Add(new ContentZombieOutbreakEvent(tile));

        m_hasInit = true;
    }

    public static GameEvent GetRandomEvent(GameTile tile)
    {
        if (!m_hasInit)
        {
            Init(tile);
        }

        int r = UnityEngine.Random.Range(0, m_events.Count);

        return (GameEvent)Activator.CreateInstance(m_events[r].GetType(), tile);
    }

    public static GameEvent GetEventFromJson(JsonGameEventData jsonData)
    {
        int i = m_events.FindIndex(t => t.m_name == jsonData.name);

        GameEvent newBuilding = (GameEvent)Activator.CreateInstance(m_events[i].GetType());
        newBuilding.LoadFromJson(jsonData);

        return newBuilding;
    }
}