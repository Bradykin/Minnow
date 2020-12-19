using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMagicianEvent : GameEvent
{
    public ContentMagicianEvent(GameTile tile)
    {
        m_name = "Wandering Magician";
        m_eventDesc = "A wandering magician travels around here. He frequently offers his service in the name of stability in the land.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventStatsBuffOption(m_tile, 8, 15);
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardSpellCard(GameElementBase.GameRarity.Rare, GameHelper.GetPlayer().m_deckBase.GetCardsForRead()));

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Give the unit that goes here +8/+15.";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Gain a random rare spell card.";
    }
}