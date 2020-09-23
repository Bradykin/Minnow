using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRogueEvent : GameEvent
{
    public ContentRogueEvent(GameTile tile)
    {
        m_name = "Wandering Rogue";
        m_eventDesc = "A wandering rogue stops your troops on the side of the road, and offers you a trade.";
        m_tile = tile;
        m_rarity = GameRarity.Uncommon;

        m_optionOne = new GameEventTransformCardOption(UIDeckViewController.DeckViewFilter.Entities);
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardCard());
        m_optionThree = new GameEventLeaveOption();

        m_minWaveToSpawn = 1;
        m_maxWaveToSpawn = Constants.FinalWaveNum;

        LateInit();
    }
}