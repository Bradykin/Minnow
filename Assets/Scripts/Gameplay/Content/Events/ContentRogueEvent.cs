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
        m_rarity = GameRarity.Common;

        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardSpellCard());
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardSpellCard());
        m_optionThree = new GameEventLeaveOption();

        m_minWaveToSpawn = 1;
        m_maxWaveToSpawn = Constants.FinalWaveNum;

        LateInit();
    }
}