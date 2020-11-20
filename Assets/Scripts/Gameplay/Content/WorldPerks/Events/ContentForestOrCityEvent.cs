using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForestOrCityEvent : GameEvent
{
    public ContentForestOrCityEvent(GameTile tile)
    {
        m_name = "Forest Or City";
        m_eventDesc = "Two rival tribes offer visitors knowledge of either the forest or the city.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventGiveKeywordOption(m_tile, new GameForestwalkKeyword());
        m_optionTwo = new GameEventGiveKeywordOption(m_tile, new GameKnowledgeableKeyword(new GameGainStaminaAction(m_tile.GetOccupyingUnit(), 1)));

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Give the unit that goes here <b>Forestwalk</b>.";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Give the unit that goes here <b>Knowledgeable</b>: Gain 1 stamina'.";
    }
}