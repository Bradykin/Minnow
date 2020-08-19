﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : GameElementBase
{
    public GameDeck m_deck { get; private set; }
    public GameDeck m_curDeck { get; private set; }

    public List<GameCard> m_hand { get; private set; }

    public List<GameEntity> m_controlledEntities { get; private set; }

    public GamePlayer()
    {
        m_deck = new GameDeck();
        m_curDeck = new GameDeck();
        m_hand = new List<GameCard>();
        m_controlledEntities = new List<GameEntity>();

        ResetCurDeck();

        m_curDeck.Shuffle();

        for (int i = 0; i < Constants.InitialHandSize; i++)
        {
            m_hand.Add(m_curDeck.DrawCard());
        }
    }

    private void ResetCurDeck()
    {
        for (int i = 0; i < m_deck.Count(); i++)
        {
            m_curDeck.AddCard(m_deck.GetCardByIndex(i));
        }
    }

    public void AddControlledEntity(GameEntity toAdd)
    {
        m_controlledEntities.Add(toAdd);
    }

    public void RemoveControlledEntity(GameEntity toRemove)
    {
        m_controlledEntities.Remove(toRemove);
    }
}
