﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCombatTrainingEvent : GameEvent
{
    public ContentCombatTrainingEvent(GameTile tile)
    {
        m_name = "Combat Training";
        m_eventDesc = "An old human man approaches your troops.  You recognize him as one of the master swordsmen of the region, the legendary Alduo.  He offers to train some of your troops on the style of your choice.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventGiveKeywordOption(m_tile, new GameEnrageKeyword(new GameGainStatsAction(m_tile.m_occupyingUnit, 0, 3)));
        m_optionTwo = new GameEventGiveKeywordOption(m_tile, new GameVictoriousKeyword(new GameGainStatsAction(m_tile.m_occupyingUnit, 5, 0)));

        base.LateInit();
    }
}