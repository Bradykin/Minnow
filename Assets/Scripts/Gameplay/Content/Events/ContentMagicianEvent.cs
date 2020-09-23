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
        m_rarity = GameRarity.Uncommon;

        m_optionOne = new GameEventTransformCardOption(UIDeckViewController.DeckFilterType.Entities);
        m_optionTwo = new GameEventDuplicateCardOption(UIDeckViewController.DeckFilterType.Spells);
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardSpellCard(GameHelper.GetPlayer().m_deckBase.GetCardsForRead()));

        LateInit();

        m_minWaveToSpawn = 1;
        m_maxWaveToSpawn = Constants.FinalWaveNum;
    }
}