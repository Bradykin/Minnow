using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDorphinAltar : GameAltar
{
    public ContentDorphinAltar(GameTile tile)
    {
        m_name = "Dorphin";
        m_eventDesc = "An altar of Dorphin.  Behold the power of gold.";
        m_tile = tile;
        m_rarity = GameRarity.Special;

        m_altarRelic = new ContentGreedOfDorphinRelic();

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeSpecificRelicOption(m_altarRelic);

        WorldGridManager.Instance.ClearAltars();

        base.LateInit();
    }
}
