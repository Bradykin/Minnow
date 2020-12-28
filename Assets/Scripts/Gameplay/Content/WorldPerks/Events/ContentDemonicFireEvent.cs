using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonicFireEvent : GameEvent
{
    public ContentDemonicFireEvent(GameTile tile)
    {
        m_name = "Demonic Fire";
        m_eventDesc = "Reports of a mysterious ghostly fire flying around this region flood in.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventConsumeFireOption(m_tile, 3);
        m_optionTwo = new GameEventDuplicateCardOption(UIDeckViewController.DeckViewFilter.Spells);

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Give the unit that goes here '<b>Enrage</b>: +3/+0' <b>permanently</b>.";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Duplicate a spell card <b>permanently</b>.";
    }
}

public class GameEventConsumeFireOption : GameEventOption
{
    private GameTile m_tile;
    private int m_toGrow;

    public GameEventConsumeFireOption(GameTile tile, int toGrow)
    {
        m_tile = tile;
        m_toGrow = toGrow;
    }

    public override void Init()
    {
        m_message = $"{m_tile.GetOccupyingUnit().GetName()} gains '<b>Enrage</b>: Gain +{m_toGrow}/+0' <b>permanently</b> ";
    }

    public override void AcceptOption()
    {
        m_tile.GetOccupyingUnit().AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(m_tile.GetOccupyingUnit(), m_toGrow, 0)), true, false);

        EndEvent();
    }

    //Intentionally left blank
    public override void DeclineOption()
    {

    }
}