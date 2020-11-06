using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMonAltar : GameEvent
{
    public ContentMonAltar(GameTile tile)
    {
        m_name = "Mon";
        m_eventDesc = "An altar of Mon.  Behold the power of tactics.";
        m_tile = tile;
        m_rarity = GameRarity.Special;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeSpecificRelicOption(new ContentTacticsOfMonRelic());

        WorldGridManager.Instance.ClearAltars();

        base.LateInit();
    }
}
