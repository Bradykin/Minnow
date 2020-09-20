using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForbiddenFruitEvent : GameEvent
{
    public ContentForbiddenFruitEvent(GameTile tile)
    {
        m_name = "Forbidden Fruit";
        m_eventDesc = "You stumble across a garden with fruit you aren't supposed to eat. Yes, that garden. Will you follow the rules and reject the fruit, or are you going to take it for yourself?";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        m_optionOne = new GameEventRejectFruitOption(tile);
        m_optionTwo = new GameEventEatFruitOption(tile);
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}