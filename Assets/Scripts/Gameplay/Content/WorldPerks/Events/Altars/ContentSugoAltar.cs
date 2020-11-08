using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSugoAltar : GameAltar
{
    public ContentSugoAltar(GameTile tile)
    {
        m_name = "Sugo";
        m_eventDesc = "An altar of Sugo.  Behold the power of strength.";
        m_tile = tile;
        m_rarity = GameRarity.Special;

        m_altarRelic = new ContentMightOfSugoRelic();

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeSpecificRelicOption(m_altarRelic);

        WorldGridManager.Instance.ClearAltars();

        base.LateInit();
    }
}
