using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLibraryOfDenumianEvent : GameEvent
{
    public ContentLibraryOfDenumianEvent(GameTile tile)
    {
        m_name = "Library of Denumian";
        m_eventDesc = "The old library of Denumian lays abandoned except for a single large owl who offers gifts to those who stop by seeking knowledge.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeSpecificRelicOption(new ContentDominerickRefrainRelic());
        m_optionTwo = new GameEventTakeSpecificRelicOption(new ContentTomeOfDuluhainRelic());

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Gain the relic Dominerick Refrain.\n" + new ContentDominerickRefrainRelic().GetDesc();
    }

    public override string GetOptionTwoTooltip()
    {
        return "Gain the relic Tome of Duluhain.\n" + new ContentTomeOfDuluhainRelic().GetDesc();
    }
}