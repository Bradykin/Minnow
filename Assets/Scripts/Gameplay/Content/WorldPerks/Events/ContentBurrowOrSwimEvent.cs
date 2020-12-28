using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBurrowOrSwimEvent : GameEvent
{
    public ContentBurrowOrSwimEvent(GameTile tile)
    {
        m_name = "Burrow Or Swim";
        m_eventDesc = "A water mole lives here, suposedly constantly chanting:\n'Burrow or swim!  Swim or burrow!'\n'CHOOOOOOOOSE!'";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventGiveKeywordOption(m_tile, new GameWaterwalkKeyword());
        m_optionTwo = new GameEventGiveKeywordOption(m_tile, new GameMountainwalkKeyword());

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Give the unit that goes here <b>Waterwalk</b> <b>permanently</b>.";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Give the unit that goes here <b>Mountainwalk</b> <b>permanently</b>.";
    }
}