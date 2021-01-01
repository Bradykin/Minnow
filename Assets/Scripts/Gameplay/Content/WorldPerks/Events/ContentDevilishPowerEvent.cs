using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDevilishPowerEvent : GameEvent
{
    public ContentDevilishPowerEvent(GameTile tile)
    {
        m_name = "Devilish Power";
        m_eventDesc = "2 flaming contracts appear before all who appear here offering great power; at seemingly no cost.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventCardSelectOption(new ContentLivingBombCard());
        m_optionTwo = new GameEventGiveKeywordOption(m_tile, new GameEnrageKeyword(new GameGainGoldAction(15)));

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return $"Gain the Living Bomb spell card.\n{new ContentLivingBombCard().GetDesc()}";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Give the unit that goes here '<b>Enrage</b>: Gain 15 gold.' <b>permanently</b>.";
    }
}