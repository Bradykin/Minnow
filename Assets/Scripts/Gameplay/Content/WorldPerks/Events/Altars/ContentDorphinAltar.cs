using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDorphinAltar : GameEvent
{
    public ContentDorphinAltar(GameTile tile)
    {
        m_name = "Dorphin";
        m_eventDesc = "An altar of Dorphin.  Behold the power of gold.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeSpecificRelicOption(new ContentGreedOfDorphinRelic());

        WorldGridManager.Instance.ClearAltars();

        base.LateInit();
    }
}
