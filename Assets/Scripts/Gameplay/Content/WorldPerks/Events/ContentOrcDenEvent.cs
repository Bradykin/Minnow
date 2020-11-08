using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ContentOrcDenEvent : GameEvent
{
    public ContentOrcDenEvent(GameTile tile)
    {
        m_name = "Orc Den";
        m_eventDesc = "Scouts report tribes of Orcs in this region. While they have not yet joined the fray, they may soon. Striking first would allow some much needed plunder for the war.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventOrcGoldOption(m_tile);
        m_optionTwo = new GameEventOrcRelicOption(m_tile);

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Gain 100 gold, but spawn 2 Orc enemy units nearby.";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Gain a random rare relic, but spawn 4 Orc enemy units nearby.";
    }
}