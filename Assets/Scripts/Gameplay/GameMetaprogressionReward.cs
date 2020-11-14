using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMetaprogressionReward
{
    private string m_title;
    private string m_desc;

    private List<GameRelic> m_relics;
    private List<GameCard> m_cards;
    private GameMap m_map;
    private GameCard m_starterCard;
    private GameRelic m_starterRelic;

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
}
