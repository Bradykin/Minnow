using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldPerk
{
    public enum WorldPerkType
    { 
        Chest,
        Event,
        Altar
    }

    private WorldPerkType m_perkType;

    //For Chest
    private GameElementBase.GameRarity m_chestRarity;

    //For Event
    private GameEvent m_event;

    //For Altar
    private GameEvent m_altar;

    public GameWorldPerk(GameElementBase.GameRarity chestRarity)
    {
        m_perkType = WorldPerkType.Chest;
        m_chestRarity = chestRarity;
    }

    public GameWorldPerk(GameEvent gameEvent, bool isAltar)
    {
        if (isAltar)
        {
            m_perkType = WorldPerkType.Altar;
            m_altar = gameEvent;
        }
        else
        {
            m_perkType = WorldPerkType.Event;
            m_event = gameEvent;
        }
    }

    public void Trigger()
    {
        if (m_perkType == WorldPerkType.Chest)
        {
            UIHelper.TriggerRelicSelect(m_chestRarity);
        }
        else if (m_perkType == WorldPerkType.Event)
        {
            UIEventController.Instance.Init(m_event);
        }
        else if (m_perkType == WorldPerkType.Altar)
        {
            UIEventController.Instance.Init(m_altar);
        }
    }

    public bool IsChest()
    {
        return m_perkType == WorldPerkType.Chest;
    }

    public bool IsEvent()
    {
        return m_perkType == WorldPerkType.Event;
    }

    public bool IsAltar()
    {
        return m_perkType == WorldPerkType.Altar;
    }

    public Sprite GetIcon()
    {
        if (IsChest())
        {
            return UIHelper.GetIconChest(m_chestRarity);
        }
        else if (IsAltar())
        {
            return UIHelper.GetIconAltar(m_altar.GetName());
        }

        return null;
    }

    public GameElementBase.GameRarity GetChestRarity()
    {
        return m_chestRarity;
    }

    public GameEvent GetEvent()
    {
        return m_event;
    }

    public GameEvent GetAltar()
    {
        return m_altar;
    }
}
