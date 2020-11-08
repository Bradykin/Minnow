using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverturnedCartEvent : GameEvent
{
    public ContentOverturnedCartEvent(GameTile tile)
    {
        m_name = "Overturned Cart";
        m_eventDesc = "A cart lies by the side of the road here.";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeGoldOption(50);

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Gain 50 gold";
    }

    public override string GetOptionTwoTooltip()
    {
        return "";
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