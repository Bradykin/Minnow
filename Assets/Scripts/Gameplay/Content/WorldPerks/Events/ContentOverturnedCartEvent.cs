using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverturnedCartEvent : GameEvent
{
    public ContentOverturnedCartEvent(GameTile tile)
    {
        m_name = "Overturned Cart";
        m_eventDesc = "You find an overturned cart by the side of the road.  You should be able to grab some resources before the cart collapses!";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeGoldOption(75);
        m_optionTwo = new GameEventLeaveOption();

        base.LateInit();
    }
}

public class GameEventTakeGoldOption : GameEventOption
{
    private int m_value;

    public GameEventTakeGoldOption(int value)
    {
        m_value = value;

        m_message = "Take " + m_value + " gold.";

        m_hasTooltip = true;
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.m_wallet.m_gold += m_value;
        EndEvent();
    }
}