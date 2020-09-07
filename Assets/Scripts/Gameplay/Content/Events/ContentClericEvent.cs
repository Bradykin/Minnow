using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentClericEvent : GameEvent
{
    public ContentClericEvent(GameTile tile)
    {
        m_name = "Wandering Cleric";
        m_eventDesc = "A wandering cleric stops your troops on the side of the road, and offers to help purge some of the weakness from your spirit.";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        m_optionOne = new GameEventRemoveCardOption();
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardEntityCard());
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}

public class GameEventRemoveCardOption : GameEventOption
{
    public override string GetMessage()
    {
        m_message = "Remove a card from your deck.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        UIDeckViewController.Instance.Init(player.m_deckBase.GetDeck(), UIDeckViewController.DeckViewType.Remove);

        EndEvent();
    }
}
