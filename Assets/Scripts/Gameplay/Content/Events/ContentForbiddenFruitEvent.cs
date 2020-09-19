using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForbiddenFruitEvent : GameEvent
{
    public ContentForbiddenFruitEvent(GameTile tile)
    {
        m_name = "Forbidden Cleric";
        m_eventDesc = "A wandering cleric stops your troops on the side of the road, and offers to help purge some of the weakness from your spirit.";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        m_optionOne = new GameEventLeaveOption();
        m_optionTwo = new GameEventLeaveOption();
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}