using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRogueEvent : GameEvent
{
    public ContentRogueEvent(GameTile tile)
    {
        m_name = "Wandering Rogue";
        m_eventDesc = "A wandering rogue stops your troops on the side of the road, and offers you some wares of dubious origin.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardSpellCard(GameElementBase.GameRarity.Rare));
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardSpellCard(GameElementBase.GameRarity.Rare));
        m_optionThree = new GameEventLeaveOption();

        base.LateInit();
    }
}