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
    }
}
