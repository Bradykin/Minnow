using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndTurnButton : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;

    void OnMouseDown()
    {
        Globals.m_selectedCard = null;
        Globals.m_selectedEntity = null;

        WorldController.Instance.MoveToNextTurn();

        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("End Turn", "This will refresh your energy and regen some AP for your units.  You will also discard your hand a draw a new one.  Your enemies will all take their turns."));
    }
}
