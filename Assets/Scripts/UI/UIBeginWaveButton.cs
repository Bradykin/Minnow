using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBeginWaveButton : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;

    void OnMouseDown()
    {
        BeginWave();
    }

    private void BeginWave()
    {
        if (!Globals.m_canSelect)
        {
            return;
        }

        UIHelper.SetDefaultTintColor(m_tintRenderer);

        WorldController.Instance.EndIntermission();
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
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Begin Wave", "Start the next wave of enemies!"));
    }
}
