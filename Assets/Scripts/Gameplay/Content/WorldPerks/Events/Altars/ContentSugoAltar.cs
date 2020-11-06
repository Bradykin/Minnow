using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSugoAltar : GameEvent
{
    public ContentSugoAltar(GameTile tile)
    {
        m_name = "Sugo";
        m_eventDesc = "An altar of Sugo.  Behold the power of strength.";
        m_tile = tile;
        m_rarity = GameRarity.Special;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeSpecificRelicOption(new ContentMightOfSugoRelic());

        WorldGridManager.Instance.ClearAltars();

        base.LateInit();
    }
}
