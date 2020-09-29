using System.Collections.Generic;

public class GameEventDuplicateCardOption : GameEventOption
{
    private UIDeckViewController.DeckViewFilter m_deckFilterType;

    public GameEventDuplicateCardOption(UIDeckViewController.DeckViewFilter deckFilterType)
    {
        m_deckFilterType = deckFilterType;
    }

    public override string GetMessage()
    {
        if (m_deckFilterType == UIDeckViewController.DeckViewFilter.Entities)
        {
            m_message = "Create a copy of a unit card in your deck!";
        }
        else if (m_deckFilterType == UIDeckViewController.DeckViewFilter.Spells)
        {
            m_message = "Create a copy of a spell card in your deck!";
        }
        else
        {
            m_message = "Create a copy of a card in your deck!";
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

        UIDeckViewController.Instance.Init(deck, UIDeckViewController.DeckViewType.Duplicate, "Duplicate a Card");

        EndEvent();
    }
}