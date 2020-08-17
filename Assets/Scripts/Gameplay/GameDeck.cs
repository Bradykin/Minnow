using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDeck
{
    private List<GameCard> m_cards = new List<GameCard>();

    public GameDeck()
    {
        for (int i = 0; i < 5; i++)
        {
            m_cards.Add(new GameGoblinCard());
        }

        for (int i = 0; i < 5; i++)
        {
            m_cards.Add(new GameCureLightWoundsCard());
        }

        for (int i = 0; i < 5; i++)
        {
            m_cards.Add(new GameMinorFireboltCard());
        }
    }

    public int Count()
    {
        return m_cards.Count;
    }

    public GameCard GetCardByIndex(int index)
    {
        return m_cards[index];
    }

    public void AddCard(GameCard card)
    {
        m_cards.Add(card);
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
        GameCard toReturn = m_cards[0];
        m_cards.RemoveAt(0);
        return toReturn;
    }
}
