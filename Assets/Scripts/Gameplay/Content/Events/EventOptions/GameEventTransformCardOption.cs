﻿using System.Collections.Generic;

public class GameEventTransformCardOption : GameEventOption
{
    private UIDeckViewController.DeckViewFilter m_deckFilterType;

    public GameEventTransformCardOption(UIDeckViewController.DeckViewFilter deckFilterType)
    {
        m_deckFilterType = deckFilterType;
    }

    public override string GetMessage()
    {
        if (m_deckFilterType == UIDeckViewController.DeckViewFilter.Entities)
        {
            m_message = "Transform an entity in your deck into a random other entity";
        }
        else if (m_deckFilterType == UIDeckViewController.DeckViewFilter.Spells)
        {
            m_message = "Transform a spell in your deck into a random other spell";
        }
        else
        {
            m_message = "Transform a card in your deck into a random other one.";
        }

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        List<GameCard> deck = new List<GameCard>();
        if (m_deckFilterType == UIDeckViewController.DeckViewFilter.Entities)
        {
            deck = GameHelper.GetPlayerBaseDeckOfEntities();
        }
        else if (m_deckFilterType == UIDeckViewController.DeckViewFilter.Spells)
        {
            deck = GameHelper.GetPlayerBaseDeckOfSpells();
        }
        else
        {
            deck = player.m_deckBase.GetDeck();
        }

        UIDeckViewController.Instance.Init(deck, UIDeckViewController.DeckViewType.Transform, "Transform a card");

        EndEvent();
    }
}