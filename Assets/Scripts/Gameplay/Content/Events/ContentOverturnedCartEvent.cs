using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverturnedCartEvent : GameEvent
{
    public ContentOverturnedCartEvent(GameTile tile)
    {
        m_name = "Overturned Cart";
        m_eventDesc = "You find an overturned cart by the side of the road.  You should be able to grab some resources before the cart collapses.";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        m_optionOne = new GameEventTakeGoldOption(75);
        m_optionTwo = new GameEventTakeMagicOption(30);
        m_optionThree = new GameEventTakeBricksOption(50);

        LateInit();
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

public class GameEventTakeMagicOption : GameEventOption
{
    private int m_value;

    public GameEventTakeMagicOption(int value)
    {
        m_value = value;

        m_message = "Take " + m_value + " magic.";

        m_hasTooltip = true;
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.m_wallet.m_magic += m_value;
        EndEvent();
    }
}

public class GameEventTakeBricksOption : GameEventOption
{
    private int m_value;

    public GameEventTakeBricksOption(int value)
    {
        m_value = value;

        m_message = "Take " + m_value + " bricks.";

        m_hasTooltip = true;
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.m_wallet.m_bricks += m_value;
        EndEvent();
    }
}