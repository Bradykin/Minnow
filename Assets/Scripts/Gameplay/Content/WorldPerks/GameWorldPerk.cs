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
    private GameAltar m_altar;

    public GameWorldPerk(GameElementBase.GameRarity chestRarity)
    {
        m_perkType = WorldPerkType.Chest;
        m_chestRarity = chestRarity;
    }

    public GameWorldPerk(GameEvent gameEvent)
    {
        m_perkType = WorldPerkType.Event;
        m_event = gameEvent;
    }

    public GameWorldPerk(GameAltar gameAltar)
    {
        m_perkType = WorldPerkType.Altar;
        m_altar = gameAltar;
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
        else if (IsEvent())
        {
            return UIHelper.GetIconEvent();
        }

        return null;
    }

    public Sprite GetWIcon()
    {
        if (IsChest())
        {
            return UIHelper.GetWIconChest(m_chestRarity);
        }
        else if (IsAltar())
        {
            return UIHelper.GetWIconAltar(m_altar.GetName());
        }
        else if (IsEvent())
        {
            return UIHelper.GetWIconEvent();
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

    public GameAltar GetAltar()
    {
        return m_altar;
    }
}
