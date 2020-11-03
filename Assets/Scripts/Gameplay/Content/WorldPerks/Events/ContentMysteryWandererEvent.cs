using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMysteryWanderer : GameEvent
{
    public ContentMysteryWanderer(GameTile tile)
    {
        m_name = "Mystery Wanderer";
        m_eventDesc = "A stranger acosts your troops by the side of the road, and refuses to let them leave without taking one of his services.";
        m_tile = tile;

        m_optionOne = new GameEventRemoveCardOption();
        m_optionTwo = new GameEventDuplicateCardOption(UIDeckViewController.DeckViewFilter.Spells);

        LateInit();
    }
}