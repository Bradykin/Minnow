using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMagicianEvent : GameEvent
{
    public ContentMagicianEvent(GameTile tile)
    {
        m_name = "Wandering Magician";
        m_eventDesc = "A wandering magician stops your troops on the side of the road, and offers an interesting service in the name of stability in the land.";
        m_tile = tile;

        m_optionOne = new GameEventStatsBuffOption(m_tile, 7, 15);
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardSpellCard(GameHelper.GetPlayer().m_deckBase.GetCardsForRead()));

        LateInit();
    }
}