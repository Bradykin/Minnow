using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCombatTrainingEvent : GameEvent
{
    public ContentCombatTrainingEvent(GameTile tile)
    {
        m_name = "Combat Training";
        m_eventDesc = "The master swordsman Alduo lives here. He often offers to train visitors in the combat style of their choice.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventGiveKeywordOption(m_tile, new GameEnrageKeyword(new GameGainStatsAction(m_tile.GetOccupyingUnit(), 0, 3)));
        m_optionTwo = new GameEventGiveKeywordOption(m_tile, new GameVictoriousKeyword(new GameGainStatsAction(m_tile.GetOccupyingUnit(), 3, 0)));

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Give the unit that goes here '<b>Enrage</b>: +0/+3' <b>permanently</b> .";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Give the unit that goes here '<b>Victorious</b>: +3/+0' <b>permanently</b> .";
    }
}