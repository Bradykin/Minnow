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

        m_optionOne = new GameEventDuplicateCardOption();
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardSpellCard());
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