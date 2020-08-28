using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDragonDenEvent : GameEvent
{
    public ContentDragonDenEvent(GameTile tile)
    {
        m_name = "Dragon's Den";
        m_eventDesc = "You approach a strange den, and see a mound of gold!  You might be able to steal the gold and get out; or you could convinve the dragon sleeping on it to join you, but it'd probably want to keep it's gold.";
        m_tile = tile;
        m_rarity = GameRarity.Uncommon;

        m_optionOne = new GameEventTakeGoldOption(50);
        m_optionTwo = new GameEventTameDragonOption();
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}

public class GameEventTameDragonOption : GameEventOption
{
    GameCardEntityBase card;

    public GameEventTameDragonOption()
    {
        card = new ContentCaveDragonCard();

        m_message = "Tame the dragon, <b>permanently</b> adding it to your deck!";

        m_hasTooltip = true;
    }

    public override void BuildTooltip()
    {
        UIHelper.CreateEntityTooltip(card.GetEntity());
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.AddCardToDeck(card);
        EndEvent();
    }
}