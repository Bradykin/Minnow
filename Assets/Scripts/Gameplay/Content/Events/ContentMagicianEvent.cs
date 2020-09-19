using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMagicianEvent : GameEvent
{
    public ContentMagicianEvent(GameTile tile)
    {
        m_name = "Wandering Magician";
        m_eventDesc = "A wandering magician stops your troops on the side of the road, and offers an interesting service in the name of stability in the land.";
        m_tile = tile;
        m_rarity = GameRarity.Uncommon;

        m_optionOne = new GameEventTransformCardOption();
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardSpellCard(GameHelper.GetPlayer().m_deckBase.GetCardsForRead()));
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}

public class GameEventDuplicateCardOption : GameEventOption
{
    public override string GetMessage()
    {
        m_message = "Create a copy of a card in your deck!";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        UIDeckViewController.Instance.Init(player.m_deckBase.GetDeck(), UIDeckViewController.DeckViewType.Duplicate);

        EndEvent();
    }
}

public class GameEventCardSelectOption : GameEventOption
{
    private GameCard m_card;

    public GameEventCardSelectOption(GameCard card)
    {
        m_card = card;

        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        m_message = "Gain " + m_card.m_name + ".";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.AddCardToDiscard(GameCardFactory.GetCardClone(m_card), true);

        EndEvent();
    }

    public override void BuildTooltip()
    {
        if (m_card is GameCardEntityBase)
        {
            GameEntity entity = ((GameCardEntityBase)m_card).GetEntity();

            UIHelper.CreateEntityTooltip(entity);
        }
        else
        {
            UIHelper.CreateSpellTooltip(m_card);
        }
    }
}
