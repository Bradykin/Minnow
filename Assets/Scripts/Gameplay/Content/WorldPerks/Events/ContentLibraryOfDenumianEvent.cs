using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLibraryOfDenumianEvent : GameEvent
{
    public ContentLibraryOfDenumianEvent(GameTile tile)
    {
        m_name = "Library of Denumian";
        m_eventDesc = "A massive library erupts from the ground before your troops.  A large owl swoops out\n'I have watched your people since you came to this land, and wish to offer you aid. Select the aid of the Great Library of Denumian'.";
        m_tile = tile;
        m_rarity = GameRarity.Rare;

        m_optionOne = new GameEventTakeSpecificRelicOption(new ContentDominerickRefrainRelic());
        m_optionTwo = new GameEventTakeSpecificRelicOption(new ContentTomeOfDuluhainRelic());
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}