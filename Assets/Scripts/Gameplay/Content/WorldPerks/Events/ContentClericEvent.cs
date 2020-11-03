using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentClericEvent : GameEvent
{
    public ContentClericEvent(GameTile tile)
    {
        m_name = "Wandering Cleric";
        m_eventDesc = "A wandering cleric stops your troops on the side of the road, and offers to help purge some of the weakness from your spirit.";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        m_optionOne = new GameEventRemoveCardOption();
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardSpellCard());
        m_optionThree = new GameEventLeaveOption();

        m_minWaveToSpawn = 4;
        m_maxWaveToSpawn = 6;

        LateInit();
    }
}
