using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ContentOrcDenEvent : GameEvent
{
    public ContentOrcDenEvent(GameTile tile)
    {
        m_name = "Orc Den";
        m_eventDesc = "You see a nest of sleeping orcs. There's a couple orcs away from the group with a small treasure chest, but you can see far more lucrative treasure at the center of the group. What will you do?";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventOrcGoldOption(m_tile);
        m_optionTwo = new GameEventOrcRelicOption(m_tile);
        m_optionThree = new GameEventLeaveOption();

        base.LateInit();
    }
}