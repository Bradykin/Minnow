using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : GameElementBase
{
    public GameDeck m_deckBase { get; private set; }
    public GameDeck m_curDeck { get; private set; }

    public List<GameCard> m_hand { get; private set; }

    public List<GameEntity> m_controlledEntities { get; private set; }

    public GamePlayer()
    {
        m_deckBase = new GameDeck();
        m_deckBase.FillStartingDeck();
        m_curDeck = new GameDeck();
        m_hand = new List<GameCard>();
        m_controlledEntities = new List<GameEntity>();

        ResetCurDeck();

        m_curDeck.Shuffle();

        DrawHand();
    }

    private void DrawHand()
    {
        for (int i = 0; i < m_hand.Count; i++)
        {
            m_curDeck.AddToDiscard(m_hand[i]);
        }

        m_hand = new List<GameCard>();

        for (int i = 0; i < Constants.InitialHandSize; i++)
        {
            GameCard card = m_curDeck.DrawCard();

            if (card != null) //This can be null if the deck and discard are both empty
            {
                m_hand.Add(card);
            }
        }
    }

    public void PlayCard(GameCard card)
    {
        m_curDeck.AddToDiscard(card);
        m_hand.Remove(card);
    }

    private void ResetCurDeck()
    {
        for (int i = 0; i < m_deckBase.Count(); i++)
        {
            m_curDeck.AddCard(m_deckBase.GetCardByIndex(i));
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

    public void EndTurn()
    {
        for (int i = 0; i < m_controlledEntities.Count; i++)
        {
            m_controlledEntities[i].EndTurn();
        }

        DrawHand();
    }
}
