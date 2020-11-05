using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTelloAltar : GameEvent
{
    public ContentTelloAltar(GameTile tile)
    {
        m_name = "Tello";
        m_eventDesc = "An altar of Tello.  Behold the power of the eye.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeSpecificRelicOption(new ContentEyeOfTelloRelic());

        WorldGridManager.Instance.ClearAltars();

        base.LateInit();
    }
}
