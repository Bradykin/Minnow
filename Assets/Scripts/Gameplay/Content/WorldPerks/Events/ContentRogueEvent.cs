using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRogueEvent : GameEvent
{
    public ContentRogueEvent(GameTile tile)
    {
        m_name = "Wandering Rogue";
        m_eventDesc = "A rogue is known to travel the roads around here, and he frequently offers wares of dubious origin.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardSpellCard(GameElementBase.GameRarity.Rare));
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardSpellCard(GameElementBase.GameRarity.Rare));

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Gain a random rare spell card.";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Gain a random rare spell card.";
    }
}