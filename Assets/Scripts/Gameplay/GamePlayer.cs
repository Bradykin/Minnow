using Game.Util;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : GameElementBase, ITurns
{
    public int m_curEnergy;
    private int m_maxEnergy;

    public GameWallet m_wallet;

    public GameDeck m_deckBase { get; private set; }
    public GameDeck m_curDeck { get; private set; }

    public List<GameCard> m_hand { get; private set; }

    public List<GameEntity> m_controlledEntities { get; private set; }
    public List<GameBuildingBase> m_controlledBuildings { get; private set; }

    public GameRelicHolder m_relics;

    public GamePlayer()
    {
        m_deckBase = new GameDeck();
        m_deckBase.FillStartingDeck();
        m_curDeck = new GameDeck();
        m_hand = new List<GameCard>();
        m_controlledEntities = new List<GameEntity>();
        m_controlledBuildings = new List<GameBuildingBase>();
        m_relics = new GameRelicHolder();
        m_wallet = new GameWallet(0, 3, 10);

    }

    public void LateInit()
    {
        m_maxEnergy = Constants.StartingEnergy;
        m_curEnergy = GetMaxEnergy();

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

        int handSize = GetDrawHandSize();
        for (int i = 0; i < handSize; i++)
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
        m_curEnergy -= card.m_cost;

        if (card is GameCardSpellBase)
        {
            m_curDeck.AddToDiscard(card);
        }
        else
        {
            m_curDeck.RemoveCard(card);
        }

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

    public void AddControlledBuilding(GameBuildingBase toAdd)
    {
        m_controlledBuildings.Add(toAdd);
    }

    public void RemoveControlledEntity(GameEntity toRemove)
    {
        m_controlledEntities.Remove(toRemove);
    }

    public void RemoveControlledBuilding(GameBuildingBase toRemove)
    {
        m_controlledBuildings.Remove(toRemove);
    }

    public void AddCardToDeck(GameCard card)
    {
        m_curDeck.AddToDiscard(card);
        m_deckBase.AddCard(card);
    }

    public int GetMaxEnergy()
    {
        int toReturn = m_maxEnergy;

        toReturn += 1 * GameHelper.RelicCount<GameOrbOfEnergyRelic>();

        return toReturn;
    }

    private int GetDrawHandSize()
    {
        int toReturn = Constants.InitialHandSize;

        toReturn += 1 * GameHelper.RelicCount<GameMaskOfAgesRelic>();

        return toReturn;
    }

    //============================================================================================================//

    public void StartTurn()
    {
        Debug.Log("Start player turn");
        for (int i = 0; i < m_controlledEntities.Count; i++)
        {
            m_controlledEntities[i].StartTurn();
        }

        for (int i = 0; i < m_controlledBuildings.Count; i++)
        {
            m_controlledBuildings[i].StartTurn();
        }

        m_curEnergy = GetMaxEnergy();
    }

    public void EndTurn()
    {
        Debug.Log("End player turn");
        for (int i = 0; i < m_controlledEntities.Count; i++)
        {
            m_controlledEntities[i].EndTurn();
        }

        for (int i = 0; i < m_controlledBuildings.Count; i++)
        {
            m_controlledBuildings[i].EndTurn();
        }

        DrawHand();
    }
}
