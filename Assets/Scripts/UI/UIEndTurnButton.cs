using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndTurnButton : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            EndTurn();
        }
    }

    void OnMouseDown()
    {
        EndTurn();
    }

    private void EndTurn()
    {
        if (!Globals.m_canSelect)
        {
            return;
        }

        Globals.m_selectedCard = null;
        UIHelper.UnselectEntity();

        WorldController.Instance.MoveToNextTurn();

        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
        Globals.m_canScroll = false;
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
        Globals.m_canScroll = true;
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("End Turn", "This will refresh your energy and regen some AP for your units.  You will also discard your hand a draw a new one.  Your enemies will all take their turns."));
    }
}
