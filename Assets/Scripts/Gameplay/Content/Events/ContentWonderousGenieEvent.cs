using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWonderousGenieEvent : GameEvent
{
    public ContentWonderousGenieEvent(GameTile tile)
    {
        m_name = "Wonderous Genie";
        m_eventDesc = "A strange genie offers you a choice of two ancient relics.  Choose carefully; you may come to regret not picking the other...";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        m_optionOne = new GameEventTakeRandomRelicOption();
        m_optionTwo = new GameEventTakeRandomRelicOption(((GameEventTakeRandomRelicOption)m_optionOne).m_relic);
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}