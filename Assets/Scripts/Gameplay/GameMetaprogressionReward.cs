using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMetaprogressionReward : ILoad<JsonGameMetaProgressionRewardData>, ISave<JsonGameMetaProgressionRewardData>
{
    private string m_title;
    private string m_desc;

    private List<GameRelic> m_relics;
    private List<GameCard> m_cards;
    private GameMap m_map;
    private GameCard m_starterCard;
    private GameRelic m_starterRelic;

    public GameMetaprogressionReward()
    {

    }

    public GameMetaprogressionReward(string title, string desc, List<GameRelic> relics)
    {
        m_title = title;
        m_desc = desc;

        m_relics = relics;
    }

    public GameMetaprogressionReward(string title, string desc, List<GameCard> cards)
    {
        m_title = title;
        m_desc = desc;

        m_cards = cards;
    }

    public GameMetaprogressionReward(string title, string desc, GameMap map)
    {
        m_title = title;
        m_desc = desc;

        m_map = map;
    }

    public GameMetaprogressionReward(string title, string desc, GameCard card)
    {
        m_title = title;
        m_desc = desc;

        m_starterCard = card;
    }

    public GameMetaprogressionReward(string title, string desc, GameRelic relic)
    {
        m_title = title;
        m_desc = desc;

        m_starterRelic = relic;
    }

    public bool IsRelics()
    {
        if (m_relics == null)
        {
            return false;
        }

        return true;
    }

    public bool IsCards()
    {
        if (m_cards == null)
        {
            return false;
        }

        return true;
    }

    public bool IsMap()
    {
        if (m_map == null)
        {
            return false;
        }

        return true;
    }

    public bool IsStarterCard()
    {
        if (m_starterCard == null)
        {
            return false;
        }

        return true;
    }

    public bool IsStarterRelic()
    {
        if (m_starterRelic == null)
        {
            return false;
        }

        return true;
    }

    public List<GameRelic> GetRelics()
    {
        return m_relics;
    }

    public List<GameCard> GetCards()
    {
        return m_cards;
    }

    public GameMap GetMap()
    {
        return m_map;
    }

    public GameCard GetStarterCard()
    {
        return m_starterCard;
    }

    public GameRelic GetStarterRelic()
    {
        return m_starterRelic;
    }

    public string GetTitle()
    {
        return m_title;
    }

    public string GetDesc()
    {
        return m_desc;
    }

    public bool HasCard(GameCard toCheck)
    {
        for (int i = 0; i < m_cards.Count; i++)
        {
            if (m_cards[i].GetBaseName() == toCheck.GetBaseName())
            {
                return true;
            }
        }

        return false;
    }

    public bool HasRelic(GameRelic toCheck)
    {
        for (int i = 0; i < m_relics.Count; i++)
        {
            if (m_relics[i].GetBaseName() == toCheck.GetBaseName())
            {
                return true;
            }
        }

        return false;
    }

    public bool HasMap(GameMap toCheck)
    {
        if (m_map.m_id == toCheck.m_id)
        {
            return true;
        }

        return false;
    }

    public void LoadFromJson(JsonGameMetaProgressionRewardData jsonData)
    {
        m_title = jsonData.title;
        m_desc = jsonData.desc;

        if (jsonData.jsonGameCardDatas != null && jsonData.jsonGameCardDatas.Count > 0)
        {
            m_cards = new List<GameCard>();
            for (int i = 0; i < jsonData.jsonGameCardDatas.Count; i++)
            {
                GameCard newCard = GameCardFactory.GetCardFromJson(jsonData.jsonGameCardDatas[i]);
                m_cards.Add(newCard);
            }
        }
        else if (jsonData.jsonGameRelicDatas != null && jsonData.jsonGameRelicDatas.Count > 0)
        {
            m_relics = new List<GameRelic>();
            for (int i = 0; i < jsonData.jsonGameRelicDatas.Count; i++)
            {
                GameRelic newRelic = GameRelicFactory.GetRelicFromJson(jsonData.jsonGameRelicDatas[i]);
                m_relics.Add(newRelic);
            }
        }
        else if (jsonData.mapId >= 0)
        {
            m_map = GameMapFactory.GetMapById(jsonData.mapId);
        }
        else if (jsonData.jsonStarterGameCardData != null)
        {
            GameCard newCard = GameCardFactory.GetCardFromJson(jsonData.jsonStarterGameCardData);
            m_starterCard = newCard;
        }
        else if (jsonData.jsonStarterGameRelicData != null)
        {
            GameRelic newRelic = GameRelicFactory.GetRelicFromJson(jsonData.jsonStarterGameRelicData);
            m_starterRelic = newRelic;
        }
    }

    public JsonGameMetaProgressionRewardData SaveToJson()
    {
        JsonGameMetaProgressionRewardData jsonData = new JsonGameMetaProgressionRewardData
        {
            title = m_title,
            desc = m_desc,
            mapId = -9999
        };

        if (IsCards())
        {
            jsonData.jsonGameCardDatas = new List<JsonGameCardData>();
            for (int i = 0; i < m_cards.Count; i++)
            {
                jsonData.jsonGameCardDatas.Add(m_cards[i].SaveToJson());
            }
        }
        else if (IsRelics())
        {
            jsonData.jsonGameRelicDatas = new List<JsonGameRelicData>();
            for (int i = 0; i < m_relics.Count; i++)
            {
                jsonData.jsonGameRelicDatas.Add(m_relics[i].SaveToJson());
            }
        }
        else if (IsMap())
        {
            jsonData.mapId = m_map.m_id;
        }
        else if (IsStarterCard())
        {
            jsonData.jsonStarterGameCardData = m_starterCard.SaveToJson();
        }
        else if (IsStarterRelic())
        {
            jsonData.jsonStarterGameRelicData = m_starterRelic.SaveToJson();
        }

        return jsonData;
    }
}
