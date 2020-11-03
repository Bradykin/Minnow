using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBurrowOrSwimEvent : GameEvent
{
    public ContentBurrowOrSwimEvent(GameTile tile)
    {
        m_name = "Burrow Or Swim";
        m_eventDesc = "A water mole appears before you, and begins to chant.\n'Burrow or swim!  Swim or burrow!'\n'CHOOOOOOOOSE!'";
        m_tile = tile;

        if (m_tile == null)
        {
            return;
        }

        m_optionOne = new GameEventGiveKeywordOption(m_tile, new GameWaterwalkKeyword());
        m_optionTwo = new GameEventGiveKeywordOption(m_tile, new GameMountainwalkKeyword());

        LateInit();
    }
}