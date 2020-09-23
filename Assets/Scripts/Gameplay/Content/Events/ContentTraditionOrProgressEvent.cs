using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTraditionOrProgressEvent : GameEvent
{
    public ContentTraditionOrProgressEvent(GameTile tile)
    {
        m_name = "Tradition or Progress";
        m_eventDesc = "Sometimes life gives you a choice: you can honour the traditions of the past or forge forward for new opportunities. What kind of person are you?";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        m_optionOne = new GameEventTakeSpecificRelicOption(new ContentTraditionalMethodsRelic());
        m_optionTwo = new GameEventTakeSpecificRelicOption(new ContentNewInvestmentsRelic());
        m_optionThree = new GameEventLeaveOption();

        LateInit();

        m_minWaveToSpawn = 1;
        m_maxWaveToSpawn = 2;
    }

    public override bool IsValidToSpawn(GameTile tile)
    {
        if (GameHelper.RelicCount<ContentTraditionalMethodsRelic>() > 0 || GameHelper.RelicCount<ContentNewInvestmentsRelic>() > 0)
        {
            return false;
        }

        return base.IsValidToSpawn(tile);
    }
}