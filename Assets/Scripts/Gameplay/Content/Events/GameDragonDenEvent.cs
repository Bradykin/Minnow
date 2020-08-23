﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDragonDenEvent : GameEvent
{
    public GameDragonDenEvent(GameTile tile)
    {
        m_name = "Dragon's Den";
        m_desc = "A strange den lies here... I wonder what could be inside?";
        m_eventDesc = "You approach the dragons den, and see a mound of gold!  You might be able to steal the gold and get out; or you tame the dragon, but lose the gold for some reason!";
        m_tile = tile;
        m_icon = UIHelper.GetIconEvent(m_name);

        m_APCost = 2;

        m_optionOne = new GameEventTakeDragonGoldOption();
        m_optionTwo = new GameEventTameDragonOption();
        m_optionThree = new GameEventLeaveOption();
    }
}

public class GameEventTakeDragonGoldOption : GameEventOption
{
    private int m_gold = 50;

    public GameEventTakeDragonGoldOption()
    {
        m_message = "Take " + m_gold + " gold from the dragon!";
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.m_wallet.m_gold += m_gold;
        EndEvent();
    }
}

public class GameEventTameDragonOption : GameEventOption
{
    GameCardEntityBase card;

    public GameEventTameDragonOption()
    {
        card = new GameCaveDragonCard();

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