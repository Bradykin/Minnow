using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTraditionOrProgressEvent : GameEvent
{
    public ContentTraditionOrProgressEvent(GameTile tile)
    {
        m_name = "Tradition or Progress";
        m_eventDesc = "Elders from various villages in the region gather here to decide future plans. The choice could greatly affect the path of the war.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeSpecificRelicOption(new ContentTraditionalMethodsRelic());
        m_optionTwo = new GameEventTakeSpecificRelicOption(new ContentNewInvestmentsRelic());

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Gain the relic Traditional Methods.\n" + new ContentTraditionalMethodsRelic().GetDesc();
    }

    public override string GetOptionTwoTooltip()
    {
        return "Gain the relic New Investments.\n" + new ContentNewInvestmentsRelic().GetDesc();
    }
}