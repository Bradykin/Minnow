using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldPerk : ILoad<JsonGameWorldPerkData>, ISave<JsonGameWorldPerkData>
{
    private GameTile m_gameTile;
    
    public enum WorldPerkType : int
    { 
        Chest,
        Event,
        Altar,
        Gold
    }

    private WorldPerkType m_perkType;

    //For Chest
    private GameElementBase.GameRarity m_chestRarity;

    //For Event
    private GameEvent m_event;

    //For Altar
    private GameAltar m_altar;

    //For Gold
    private int m_goldVal;

    public GameWorldPerk(GameTile gameTile)
    {
        m_gameTile = gameTile;
    }

    public GameWorldPerk(GameTile gameTile, GameElementBase.GameRarity chestRarity)
    {
        m_gameTile = gameTile;
        m_perkType = WorldPerkType.Chest;
        m_chestRarity = chestRarity;
    }

    public GameWorldPerk(GameTile gameTile, GameEvent gameEvent)
    {
        m_gameTile = gameTile;
        m_perkType = WorldPerkType.Event;
        m_event = gameEvent;
    }

    public GameWorldPerk(GameTile gameTile, GameAltar gameAltar)
    {
        m_gameTile = gameTile;
        m_perkType = WorldPerkType.Altar;
        m_altar = gameAltar;
    }

    public GameWorldPerk(GameTile gameTile, int goldVal)
    {
        m_gameTile = gameTile;
        m_perkType = WorldPerkType.Gold;
        m_goldVal = goldVal;
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
            GameHelper.GetPlayer().m_numEventsTriggered++;
        }
        else if (m_perkType == WorldPerkType.Altar)
        {
            UIEventController.Instance.Init(m_altar);
            GameHelper.GetPlayer().m_obtainedAltar = m_altar;
        }
        else if (m_perkType == WorldPerkType.Gold)
        {
            GameHelper.GetPlayer().GainGold(m_goldVal, false);
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

    public bool IsGold()
    {
        return m_perkType == WorldPerkType.Gold;
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
        else if (IsGold())
        {
            return UIHelper.GetIconWorldPerkGold(GetGoldVal());
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
            return UIHelper.GetIconAltar(m_altar.GetName() + "W");
        }
        else if (IsEvent())
        {
            return UIHelper.GetWIconEvent();
        }
        else if (IsGold())
        {
            return UIHelper.GetIconWorldPerkGold(GetGoldVal());
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

    public int GetGoldVal()
    {
        return m_goldVal;
    }

    //============================================================================================================//

    public JsonGameWorldPerkData SaveToJson()
    {
        JsonGameWorldPerkData jsonData = new JsonGameWorldPerkData
        {
            perkType = (int)m_perkType,
            chestRarity = (int)m_chestRarity,
            gameEventData = null,
            gameAltarData = null,
            goldValue = m_goldVal
        };

        if (m_event != null)
        {
            jsonData.gameEventData = m_event.SaveToJson();
        }

        if (m_altar != null)
        {
            jsonData.gameAltarData = m_altar.SaveToJson();
        }

        return jsonData;
    }

    public void LoadFromJson(JsonGameWorldPerkData jsonData)
    {
        m_perkType = (WorldPerkType)jsonData.perkType;
        m_chestRarity = (GameElementBase.GameRarity)jsonData.chestRarity;

        if (m_perkType == WorldPerkType.Event && jsonData.gameEventData == null)
        {
            m_event = GameEventFactory.GetRandomEvent(m_gameTile);
        }
        else if (jsonData.gameEventData != null)
        {
            m_event = GameEventFactory.GetEventFromJson(jsonData.gameEventData, m_gameTile);
        }

        if (jsonData.gameAltarData != null)
        {
            m_altar = (GameAltar)GameEventFactory.GetEventFromJson(jsonData.gameAltarData, m_gameTile);
        }

        m_goldVal = jsonData.goldValue;
    }
}
