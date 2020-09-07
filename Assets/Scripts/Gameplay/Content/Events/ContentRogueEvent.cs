using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRogueEvent : GameEvent
{
    public ContentRogueEvent(GameTile tile)
    {
        m_name = "Wandering Rogue";
        m_eventDesc = "A wandering rogue stops your troops on the side of the road, and offers you a trade.";
        m_tile = tile;
        m_rarity = GameRarity.Uncommon;

        m_optionOne = new GameEventTransformCardOption();
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardCard());
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}

public class GameEventTransformCardOption : GameEventOption
{
    public override string GetMessage()
    {
        m_message = "Transform a card in your deck into a random other one.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        UIDeckViewController.Instance.Init(player.m_deckBase.GetDeck(), UIDeckViewController.DeckViewType.Transform);

        EndEvent();
    }
}
