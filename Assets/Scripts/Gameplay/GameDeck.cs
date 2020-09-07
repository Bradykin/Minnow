using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDeck
{
    private List<GameCard> m_cards = new List<GameCard>();
    private List<GameCard> m_discard = new List<GameCard>();

    public GameDeck()
    {
    }

    public void FillStartingDeck()
    {
        for (int i = 0; i < 3; i++)
        {
            m_cards.Add(new ContentGoblinCard());
        }

        for (int i = 0; i < 1; i++)
        {
            m_cards.Add(new ContentInjuredTrollCard());
        }

        for (int i = 0; i < 1; i++)
        {
            m_cards.Add(new ContentGroundskeeperCard());
        }

        for (int i = 0; i < 2; i++)
        {
            m_cards.Add(new ContentCureWoundsCard());
        }

        for (int i = 0; i < 2; i++)
        {
            m_cards.Add(new ContentFireboltCard());
        }

        for (int i = 0; i < 1; i++)
        {
            m_cards.Add(new ContentGrowTalonsCard());
        }

        for (int i = 0; i < 1; i++)
        {
            m_cards.Add(new ContentArcaneBoltCard());
        }
    }

    public List<GameCard> GetDeck()
    {
        return m_cards;
    }

    public List<GameCard> GetDiscard()
    {
        return m_discard;
    }

    public int Count()
    {
        return m_cards.Count;
    }

    public int DiscardCount()
    {
        return m_discard.Count;
    }

    public void ClearDeck()
    {
        m_cards = new List<GameCard>();
    }

    public GameCard GetCardByIndex(int index)
    {
        return m_cards[index];
    }

    public void RemoveCard(GameCard toRemove)
    {
        m_cards.Remove(toRemove);
        m_discard.Remove(toRemove);
    }

    public void AddCard(GameCard card)
    {
        m_cards.Add(card);
    }

    public void AddToDiscard(GameCard card)
    {
        m_discard.Add(card);
    }

    public void Shuffle()
    {
        for (int i = 0; i < m_cards.Count; i++)
        {
            GameCard temp = m_cards[i];
            int randomIndex = Random.Range(i, m_cards.Count);
            m_cards[i] = m_cards[randomIndex];
            m_cards[randomIndex] = temp;
        }
    }

    public GameCard DrawCard()
    {
        if (m_cards.Count == 0)
        {
            ShuffleDiscard();
        }

        if (m_cards.Count == 0) //This means that both the deck and the discard are empty
        {
            return null;
        }

        GameCard toReturn = m_cards[0];
        m_cards.RemoveAt(0);
        return toReturn;
    }

    private void ShuffleDiscard()
    {
        m_cards.AddRange(m_discard);
        m_discard = new List<GameCard>();

        Shuffle();
    }
}
