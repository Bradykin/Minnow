using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : GameElementBase
{
    public GameTile m_tile;
    public int m_APCost;
    public string m_eventDesc;

    public bool m_isComplete;

    public GameEventOption m_optionOne;
    public GameEventOption m_optionTwo;
    public GameEventOption m_optionThree;

    public override UITooltipController InitTooltip()
    {
        UITooltipController tooltipController = base.InitTooltip();

        string apString = "AP Cost: " + m_APCost;
        tooltipController.m_descText.text += "\n" + apString;

        return tooltipController;
    }
}
