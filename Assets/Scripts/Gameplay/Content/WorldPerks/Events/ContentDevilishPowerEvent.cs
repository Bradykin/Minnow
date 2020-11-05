using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDevilishPowerEvent : GameEvent
{
    public ContentDevilishPowerEvent(GameTile tile)
    {
        m_name = "Devilish Power";
        m_eventDesc = "A burst of flame, a jolt of heat, and 2 contracts appear before you.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventCardSelectOption(new ContentLivingBombCard());
        m_optionTwo = new GameEventGiveKeywordOption(m_tile, new GameEnrageKeyword(new GameGainGoldEnrageAction(m_tile.m_occupyingUnit, 1)));

        base.LateInit();
    }
}